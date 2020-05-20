using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Events;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
using StudyProgress.Api.Dtos;
using StudyProgress.Common.Data;
using StudyProgress.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Services
{
    public interface IProgramService
    {
        public Task<List<ProgramReadDto>> GetAllAsync();
        public Task<ProgramReadDto> GetAsync(string programId);
        public Task UpdateAsync(string programId, ProgramUpdateDto dto);
        public Task<ProgramReadDto> CreateAsync(ProgramCreateDto dto);
        public Task<ProgramReadDto> DeleteAsync(string programId);

        public Task<List<CourseReadDto>> GetCoursesAsync(string programId);
        public Task<CourseReadDto> AddCourseAsync(string programId, string courseId);
        public Task<CourseReadDto> RemoveCourseAsync(string programId, string courseId);
    }

    public class ProgramService : ServiceBase, IProgramService
    {
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;

        public ProgramService(StudyProgressContext context, IMapper mapper, IMessagePublisher messagePublisher) : base(context)
        {
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        #region Programs
        public async Task<List<ProgramReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<ProgramReadDto>(_context.Set<Common.Models.Program>()).ToListAsyncFallback();
        }

        public async Task<ProgramReadDto> GetAsync(string programId)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task UpdateAsync(string programId, ProgramUpdateDto dto)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);

            _mapper.Map(dto, program);

            _context.Update(program);
            await _context.SaveChangesAsync();

            // Publish Updated event
            var e = _mapper.Map<ProgramUpdated>(program);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
        }

        public async Task<ProgramReadDto> CreateAsync(ProgramCreateDto dto)
        {
            await ValidateDuplicationAsync<Common.Models.Program>(dto.Id);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            var program = _mapper.Map<Common.Models.Program>(dto);

            await _context.AddAsync(program);
            await _context.SaveChangesAsync();

            // Publish Created event
            var e = _mapper.Map<ProgramCreated>(program);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> DeleteAsync(string programId)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            _context.Remove(program);
            await _context.SaveChangesAsync();

            // Publish Deleted event
            var e = _mapper.Map<ProgramDeleted>(program);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<ProgramReadDto>(program);
        }
        #endregion

        #region Courses
        public async Task<List<CourseReadDto>> GetCoursesAsync(string programId)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Collection(p => p.ProgramCourses)
                .Query()
                .Include(pc => pc.Course)
                .LoadAsync();

            return await _mapper.ProjectTo<CourseReadDto>(
                    program.ProgramCourses
                    .Select(pc => pc.Course)
                    .AsQueryable())
                .ToListAsyncFallback();
        }

        public async Task<CourseReadDto> AddCourseAsync(string programId, string courseId)
        {
            await ValidateExistenceAsync<Common.Models.Program>(programId);

            await ValidateForeignKeyAsync<Course>(courseId);

            await ValidateDuplicationAsync<ProgramCourse>(programId, courseId);

            var pc = new ProgramCourse { ProgramId = programId, CourseId = courseId };

            await _context.AddAsync(pc);
            await _context.SaveChangesAsync();

            var course = await _context.FindAsync<Course>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task<CourseReadDto> RemoveCourseAsync(string programId, string courseId)
        {
            await ValidateExistenceAsync<Common.Models.Program>(programId);

            await ValidateForeignKeyAsync<Course>(courseId);

            var pc = await _context.FindAsync<ProgramCourse>(programId, courseId);
            Validate(
                condition: !(pc is ProgramCourse),
                message: $"Course '{courseId}' is not in Program '{programId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(pc);
            await _context.SaveChangesAsync();

            var course = await _context.FindAsync<Course>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }
        #endregion
    }
}
