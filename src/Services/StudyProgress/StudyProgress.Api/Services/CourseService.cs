using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyProgress.Api.Data;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Services
{
    public interface ICourseService
    {
        public Task<List<CourseReadDto>> GetAllAsync();
        public Task<CourseReadDto> GetAsync(int courseId);
        public Task UpdateAsync(int courseId, CourseCreateUpdateDto dto);
        public Task<CourseReadDto> CreateAsync(CourseCreateUpdateDto dto);
        public Task<CourseReadDto> DeleteAsync(int courseId);

        public Task<List<ProgramReadDto>> GetProgramsAsync(int courseId);
        public Task<ProgramReadDto> AddProgramAsync(int courseId, int programId);
        public Task<ProgramReadDto> RemoveProgramAsync(int courseId, int programId);

        public Task<List<CourseReadDto>> GetRequirementsAsync(int courseId);
        public Task<CourseReadDto> AddRequirementAsync(int courseId, int requiredCourseId);
        public Task<CourseReadDto> RemoveRequirementAsync(int courseId, int requiredCourseId);
    }
    public class CourseService : ServiceBase, ICourseService
    {
        private readonly IMapper _mapper;

        public CourseService(StudyProgressContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region Courses
        public async Task<List<CourseReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<CourseReadDto>(_context.Set<Course>()).ToListAsyncFallback();
        }

        public async Task<CourseReadDto> GetAsync(int courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task UpdateAsync(int courseId, CourseCreateUpdateDto dto)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            _mapper.Map(dto, course);

            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReadDto> CreateAsync(CourseCreateUpdateDto dto)
        {
            var course = _mapper.Map<Course>(dto);

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task<CourseReadDto> DeleteAsync(int courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            _context.Remove(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }
        #endregion

        #region Programs
        public async Task<List<ProgramReadDto>> GetProgramsAsync(int courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            await _context.Entry(course)
                .Collection(c => c.ProgramCourses)
                .Query()
                .Include(pc => pc.Program)
                .LoadAsync();

            return await _mapper.ProjectTo<ProgramReadDto>(
                    course.ProgramCourses
                    .Select(pc => pc.Program)
                    .AsQueryable())
                .ToListAsyncFallback();
        }

        public async Task<ProgramReadDto> AddProgramAsync(int courseId, int programId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Models.Program>(programId);

            await ValidateDuplicationAsync<ProgramCourse>(programId, courseId);

            var pc = new ProgramCourse { ProgramId = programId, CourseId = courseId };

            await _context.AddAsync(pc);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Models.Program>(programId);

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> RemoveProgramAsync(int courseId, int programId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            await ValidateForeignKeyAsync<Models.Program>(programId);

            var pc = await _context.FindAsync<ProgramCourse>(programId, courseId);
            Validate(
                condition: !(pc is ProgramCourse),
                message: $"Program '{programId}' is not in Course '{courseId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(pc);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Models.Program>(programId);

            return _mapper.Map<ProgramReadDto>(program);
        }
        #endregion

        #region Requirements
        public async Task<List<CourseReadDto>> GetRequirementsAsync(int courseId)
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

        public async Task<CourseReadDto> AddRequirementAsync(int courseId, int requiredCourseId)
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

        public async Task<CourseReadDto> RemoveRequirementAsync(int courseId, int requiredCourseId)
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
