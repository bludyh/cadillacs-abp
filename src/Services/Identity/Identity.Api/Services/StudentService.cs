using AutoMapper;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Events;
using Identity.Api.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface IStudentService
    {
        public Task<List<StudentReadDto>> GetAllAsync();
        public Task<StudentReadDto> GetAsync(int studentId);
        public Task UpdateAsync(int studentId, StudentUpdateDto dto);
        public Task<StudentReadDto> CreateAsync(StudentCreateDto dto);
        public Task<StudentReadDto> DeleteAsync(int studentId);
        public Task<List<RoleReadDto>> GetRolesAsync(int studentId); 
        public Task<RoleReadDto> AddRoleAsync(int studentId, string roleName); 
        public Task<RoleReadDto> RemoveRoleAsync(int studentId, string roleName);
        public Task<List<StudentMentorReadDto>> GetMentorsAsync(int studentId);
        public Task<StudentMentorReadDto> AddMentorAsync(int studentId, StudentMentorCreateDto dto);
        public Task<StudentMentorReadDto> RemoveMentorAsync(int studentId, int teacherId, string mentorType);
    }

    public class StudentService : ServiceBase, IStudentService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IMessagePublisher _messagePublisher;

        public StudentService(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IMessagePublisher messagePublisher)
            : base(context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _messagePublisher = messagePublisher;
        }

        public async Task<StudentMentorReadDto> AddMentorAsync(int studentId, StudentMentorCreateDto dto)
        {
            await ValidateExistenceAsync<Student>(studentId);

            await ValidateForeignKeyAsync<Teacher>(dto.TeacherId);

            Validate(
                condition: !Enum.TryParse(dto.MentorType, true, out MentorType mentorType),
                message: $"MentorType '{dto.MentorType}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            await ValidateDuplicationAsync<Mentor>(dto.TeacherId, studentId, mentorType);

            var mentor = new Mentor { TeacherId = dto.TeacherId.Value, StudentId = studentId, MentorType = mentorType };

            await _context.AddAsync(mentor);
            await _context.SaveChangesAsync();

            await _context.Entry(mentor)
                .Reference(m => m.Teacher)
                .Query()
                .Include(t => t.School)
                .Include(t => t.Room).ThenInclude(r => r.Building)
                .LoadAsync();

            return _mapper.Map<StudentMentorReadDto>(mentor);
        }

        public async Task<RoleReadDto> AddRoleAsync(int studentId, string roleName)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            Validate(
                condition: await _userManager.IsInRoleAsync(student, roleName),
                message: $"Student '{studentId}' already has Role '{roleName}'.",
                status: StatusCodes.Status409Conflict);

            await _userManager.AddToRoleAsync(student, roleName);

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<StudentReadDto> CreateAsync(StudentCreateDto dto)
        {
            Validate(
                condition: await _userManager.FindByEmailAsync(dto.Email) != null,
                message: $"Student with email '{dto.Email}' already exists.",
                status: StatusCodes.Status409Conflict);

            await ValidateForeignKeyAsync<Models.Program>(dto.ProgramId);

            var student = _mapper.Map<Student>(dto);
            student.UserName = await ((IdentityContext)_context).GetNextPcn();

            await _userManager.CreateAsync(student);
            await _userManager.AddToRoleAsync(student, "Student");

            // Publish Created event
            var e = _mapper.Map<StudentCreated>(student);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            await _context.Entry(student)
                .Reference(s => s.Program)
                .Query()
                .Include(p => p.School)
                .LoadAsync();

            return _mapper.Map<StudentReadDto>(student);
        }

        public async Task<StudentReadDto> DeleteAsync(int studentId)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            await _context.Entry(student)
                .Reference(s => s.Program)
                .Query()
                .Include(p => p.School)
                .LoadAsync();

            _context.Remove(student);
            await _context.SaveChangesAsync();

            // Publish Deleted event
            var e = _mapper.Map<StudentDeleted>(student);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<StudentReadDto>(student);
        }

        public async Task<List<StudentReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<StudentReadDto>(_context.Set<Student>()).ToListAsyncFallback();
        }

        public async Task<StudentReadDto> GetAsync(int studentId)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            await _context.Entry(student)
                .Reference(s => s.Program)
                .Query()
                .Include(p => p.School)
                .LoadAsync();

            return _mapper.Map<StudentReadDto>(student);
        }

        public async Task<List<StudentMentorReadDto>> GetMentorsAsync(int studentId)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            await _context.Entry(student)
                .Collection(s => s.Mentors)
                .Query()
                .Include(m => m.Teacher).ThenInclude(t => t.School)
                .Include(m => m.Teacher).ThenInclude(t => t.Room).ThenInclude(r => r.Building)
                .LoadAsync();

            return await _mapper.ProjectTo<StudentMentorReadDto>(student.Mentors.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<List<RoleReadDto>> GetRolesAsync(int studentId)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            return await _mapper.ProjectTo<RoleReadDto>((await _userManager.GetRolesAsync(student)).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<StudentMentorReadDto> RemoveMentorAsync(int studentId, int teacherId, string mentorType)
        {
            await ValidateExistenceAsync<Student>(studentId);
            await ValidateForeignKeyAsync<Teacher>(teacherId);
            Validate(
                condition: !Enum.TryParse(mentorType, true, out MentorType type),
                message: $"MentorType '{mentorType}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            var mentor = await _context.FindAsync<Mentor>(teacherId, studentId, type);
            Validate(
                condition: !(mentor is Mentor),
                message: $"Student '{studentId}' does not have Teacher '{teacherId}' as '{mentorType}' mentor.",
                status: StatusCodes.Status404NotFound);

            await _context.Entry(mentor)
                .Reference(m => m.Teacher)
                .Query()
                .Include(t => t.School)
                .Include(t => t.Room).ThenInclude(r => r.Building)
                .LoadAsync();

            _context.Remove(mentor);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentMentorReadDto>(mentor);
        }

        public async Task<RoleReadDto> RemoveRoleAsync(int studentId, string roleName)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            Validate(
                condition: !await _userManager.IsInRoleAsync(student, roleName),
                message: $"Student '{studentId}' does not have Role '{roleName}'.",
                status: StatusCodes.Status404NotFound);

            await _userManager.RemoveFromRoleAsync(student, roleName);

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task UpdateAsync(int studentId, StudentUpdateDto dto)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            await ValidateForeignKeyAsync<Models.Program>(dto.ProgramId);

            _mapper.Map(dto, student);

            _context.Update(student);
            await _context.SaveChangesAsync();

            // Publish Updated event
            var e = _mapper.Map<StudentUpdated>(student);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
        }
    }
}
