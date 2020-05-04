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
        public Task<CourseReadDto> GetAsync(string courseId);
        public Task UpdateAsync(string courseId, CourseCreateUpdateDto dto);
        public Task<CourseReadDto> CreateAsync(CourseCreateUpdateDto dto);
        public Task<CourseReadDto> DeleteAsync(string courseId);

        public Task<List<ProgramReadDto>> GetProgramsAsync(string courseId);
        public Task<ProgramReadDto> AddProgramAsync(string courseId, string programId);
        public Task<ProgramReadDto> RemoveProgramAsync(string courseId, string programId);

        public Task<List<CourseReadDto>> GetRequirementsAsync(string courseId);
        public Task<CourseReadDto> AddRequirementAsync(string courseId, string requiredCourseId);
        public Task<CourseReadDto> RemoveRequirementAsync(string courseId, string requiredCourseId);

        public Task<List<ClassEnrollmentReadDto>> GetEnrollmentsAsync(string courseId, string classId, int classSemester, int classYear);
        public Task<ClassEnrollmentReadDto> AddEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId);
        public Task<ClassEnrollmentReadDto> RemoveEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId);
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

        public async Task<CourseReadDto> GetAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task UpdateAsync(string courseId, CourseCreateUpdateDto dto)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            _mapper.Map(dto, course);

            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReadDto> CreateAsync(CourseCreateUpdateDto dto)
        {
            await ValidateDuplicationAsync<Course>(dto.Id);

            var course = _mapper.Map<Course>(dto);

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task<CourseReadDto> DeleteAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            _context.Remove(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }
        #endregion

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

            await ValidateForeignKeyAsync<Models.Program>(programId);

            await ValidateDuplicationAsync<ProgramCourse>(programId, courseId);

            var pc = new ProgramCourse { ProgramId = programId, CourseId = courseId };

            await _context.AddAsync(pc);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Models.Program>(programId);

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> RemoveProgramAsync(string courseId, string programId)
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

        #region Enrollments
        public async Task<List<ClassEnrollmentReadDto>> GetEnrollmentsAsync(string courseId, string classId, int classSemester, int classYear)
        {
            var course = await ValidateExistenceAsync<Course>(courseId);

            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, courseId);

            await ValidateForeignKeyAsync<Course>(inputClass.CourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.Enrollments)
                .Query()
                .Include(e => e.Student)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassEnrollmentReadDto>(inputClass.Enrollments.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassEnrollmentReadDto> AddEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, courseId);

            await ValidateExistenceAsync<Student>(studentId);

            await ValidateForeignKeyAsync<Course>(inputClass.CourseId);

            await ValidateDuplicationAsync<Enrollment>(classId, classSemester, classYear, courseId, studentId);

            var e = new Enrollment
            {
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = courseId,
                StudentId = studentId
            };

            await _context.AddAsync(e);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassEnrollmentReadDto>(e);
        }

        public async Task<ClassEnrollmentReadDto> RemoveEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId)
        {
            await ValidateExistenceAsync<Course>(courseId);

            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, courseId);

            await ValidateExistenceAsync<Student>(studentId);

            await ValidateForeignKeyAsync<Course>(inputClass.CourseId);

            var e = await _context.FindAsync<Enrollment>(classId, classSemester, classYear, courseId, studentId);
            Validate(
                condition: !(e is Enrollment),
                message: $"Student '{studentId}' is not enrolled in Class.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(e);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassEnrollmentReadDto>(e);
        }
        #endregion
    }
}
