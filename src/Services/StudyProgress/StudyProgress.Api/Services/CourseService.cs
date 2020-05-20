using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyProgress.Api.Dtos;
using StudyProgress.Common.Data;
using StudyProgress.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Services
{
    public interface ICourseService
    {
        public Task<List<ProgramReadDto>> GetProgramsAsync(string courseId);
        public Task<ProgramReadDto> AddProgramAsync(string courseId, string programId);
        public Task<ProgramReadDto> RemoveProgramAsync(string courseId, string programId);

        public Task<List<CourseReadDto>> GetRequirementsAsync(string courseId);
        public Task<CourseReadDto> AddRequirementAsync(string courseId, string requiredCourseId);
        public Task<CourseReadDto> RemoveRequirementAsync(string courseId, string requiredCourseId);
    }
    public class CourseService : ServiceBase, ICourseService
    {
        private readonly IMapper _mapper;

        public CourseService(StudyProgressContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region Programs
        public async Task<List<ProgramReadDto>> GetProgramsAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            await _context.Entry(course)
                .Collection(c => c.ProgramCourses)
                .Query()
                .Include(pc => pc.Program).ThenInclude(p => p.School)
                .LoadAsync();

            return await _mapper.ProjectTo<ProgramReadDto>(
                    course.ProgramCourses
                    .Select(pc => pc.Program)
                    .AsQueryable())
                .ToListAsyncFallback();
        }

        public async Task<ProgramReadDto> AddProgramAsync(string courseId, string programId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Common.Models.Program>(programId);

            await ValidateDuplicationAsync<ProgramCourse>(programId, courseId);

            var pc = new ProgramCourse { ProgramId = programId, CourseId = courseId };

            await _context.AddAsync(pc);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Common.Models.Program>(programId);

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> RemoveProgramAsync(string courseId, string programId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Common.Models.Program>(programId);

            var pc = await _context.FindAsync<ProgramCourse>(programId, courseId);
            Validate(
                condition: !(pc is ProgramCourse),
                message: $"Program '{programId}' is not in Course '{courseId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(pc);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Common.Models.Program>(programId);

            return _mapper.Map<ProgramReadDto>(program);
        }
        #endregion

        #region Requirements
        public async Task<List<CourseReadDto>> GetRequirementsAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            await _context.Entry(course)
                .Collection(c => c.Requirements)
                .Query()
                .Include(r => r.RequiredCourse)
                .LoadAsync();

            return await _mapper.ProjectTo<CourseReadDto>(
                    course.Requirements
                    .Select(r => r.RequiredCourse)
                    .AsQueryable())
                .ToListAsyncFallback();
        }

        public async Task<CourseReadDto> AddRequirementAsync(string courseId, string requiredCourseId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Course>(requiredCourseId);

            await ValidateDuplicationAsync<Requirement>(courseId, requiredCourseId);

            var r = new Requirement { CourseId = courseId, RequiredCourseId = requiredCourseId };

            await _context.AddAsync(r);
            await _context.SaveChangesAsync();

            var requiredCourse = await _context.FindAsync<Course>(requiredCourseId);

            return _mapper.Map<CourseReadDto>(requiredCourse);
        }

        public async Task<CourseReadDto> RemoveRequirementAsync(string courseId, string requiredCourseId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Course>(requiredCourseId);

            var r = await _context.FindAsync<Requirement>(courseId, requiredCourseId);
            Validate(
                condition: !(r is Requirement),
                message: $"Course '{courseId}' does not have a requirement for Required Course '{requiredCourseId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(r);
            await _context.SaveChangesAsync();

            var requiredCourse = await _context.FindAsync<Course>(requiredCourseId);

            return _mapper.Map<CourseReadDto>(requiredCourse);
        }
        #endregion

    }
}
