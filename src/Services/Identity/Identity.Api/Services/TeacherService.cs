using AutoMapper;
using Identity.Api.Dtos;
using Identity.Common.Data;
using Identity.Common.Models;
using Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface ITeacherService : IEmployeeService
    {
        public Task<List<TeacherMentorReadDto>> GetMentorsAsync(int teacherId);
        public Task<TeacherMentorReadDto> AddMentorAsync(int teacherId, TeacherMentorCreateDto dto);
        public Task<TeacherMentorReadDto> RemoveMentorAsync(int teacherId, int studentId, string mentorType);
    }

    public class TeacherService : EmployeeService<Teacher>, ITeacherService
    {

        public TeacherService(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
            : base(context, mapper, userManager, roleManager) { }

        public async Task<TeacherMentorReadDto> AddMentorAsync(int teacherId, TeacherMentorCreateDto dto)
        {
            await ValidateExistenceAsync<Teacher>(teacherId);

            await ValidateForeignKeyAsync<Student>(dto.StudentId);

            Validate(
                condition: !Enum.TryParse(dto.MentorType, true, out MentorType mentorType),
                message: $"MentorType '{dto.MentorType}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            await ValidateDuplicationAsync<Mentor>(teacherId, dto.StudentId, mentorType);

            var mentor = new Mentor { TeacherId = teacherId, StudentId = dto.StudentId.Value, MentorType = mentorType };

            await _context.AddAsync(mentor);
            await _context.SaveChangesAsync();

            await _context.Entry(mentor)
                .Reference(m => m.Student)
                .Query()
                .Include(s => s.Program).ThenInclude(p => p.School)
                .LoadAsync();

            return _mapper.Map<TeacherMentorReadDto>(mentor);
        }

        public async Task<List<TeacherMentorReadDto>> GetMentorsAsync(int teacherId)
        {
            var teacher = await ValidateExistenceAsync<Teacher>(teacherId);

            await _context.Entry(teacher)
                .Collection(s => s.Mentors)
                .Query()
                .Include(m => m.Student).ThenInclude(s => s.Program).ThenInclude(p => p.School)
                .LoadAsync();

            return await _mapper.ProjectTo<TeacherMentorReadDto>(teacher.Mentors.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<TeacherMentorReadDto> RemoveMentorAsync(int teacherId, int studentId, string mentorType)
        {
            await ValidateExistenceAsync<Teacher>(teacherId);
            await ValidateForeignKeyAsync<Student>(studentId);
            Validate(
                condition: !Enum.TryParse(mentorType, true, out MentorType type),
                message: $"MentorType '{mentorType}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            var mentor = await _context.FindAsync<Mentor>(teacherId, studentId, type);
            Validate(
                condition: !(mentor is Mentor),
                message: $"Teacher '{teacherId}' is not '{mentorType}' mentor of Student '{studentId}'.",
                status: StatusCodes.Status404NotFound);

            await _context.Entry(mentor)
                .Reference(m => m.Student)
                .Query()
                .Include(s => s.Program).ThenInclude(p => p.School)
                .LoadAsync();

            _context.Remove(mentor);
            await _context.SaveChangesAsync();

            return _mapper.Map<TeacherMentorReadDto>(mentor);
        }
    }
}
