using AutoMapper;
using Course.Api.Dtos;
using Course.Common.Data;
using Course.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Services
{
    public interface ICourseService
    {
        public Task<List<CourseReadDto>> GetAllAsync();
        public Task<CourseReadDto> GetAsync(string courseId);
        public Task UpdateAsync(string courseId, CourseUpdateDto dto);
        public Task<CourseReadDto> CreateAsync(CourseCreateDto dto);
        public Task<CourseReadDto> DeleteAsync(string courseId);

        public Task<List<ClassReadDto>> GetClassesAsync(string classCourseId);
        public Task<ClassReadDto> GetClassAsync(string classCourseId, string classId, int classSemester, int classYear);
        public Task<ClassReadDto> CreateClassAsync(string classCourseId, ClassCreateDto dto);
        public Task<ClassReadDto> DeleteClassAsync(string courseId, string classId, int classSemester, int classYear);

        public Task<List<StudyMaterialReadDto>> GetStudyMaterialsAsync(string classCourseId, 
            string classId, int classSemester, int classYear);
        public Task<StudyMaterialReadDto> GetStudyMaterialAsync(string classCourseId, 
            string classId, int classSemester, int classYear, int studyMaterialId);
        public Task UpdateStudyMaterialAsync(string classCourseId, string classId, int classSemester,
            int classYear, int studyMaterialId, StudyMaterialCreateUpdateDto dto);
        public Task<StudyMaterialReadDto> CreateStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, StudyMaterialCreateUpdateDto dto);
        public Task<StudyMaterialReadDto> DeleteStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId);
    }

    public class CourseService<T> : ServiceBase, ICourseService where T : Common.Models.Course
    {
        private readonly IMapper _mapper;

        public CourseService(CourseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region Courses
        public async Task<List<CourseReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<CourseReadDto>(_context.Set<T>()).ToListAsyncFallback();
        }

        public async Task<CourseReadDto> GetAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task UpdateAsync(string courseId, CourseUpdateDto dto)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            _mapper.Map(dto, course);
            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReadDto> CreateAsync(CourseCreateDto dto)
        {
            await ValidateDuplicationAsync<T>(dto.Id);

            var course = _mapper.Map<T>(dto);

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task<CourseReadDto> DeleteAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            _context.Remove(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }
        #endregion

        #region Classes
        public async Task<List<ClassReadDto>> GetClassesAsync(string classCourseId)
        {
            var course = await ValidateExistenceAsync<T>(classCourseId);

            await _context.Entry(course)
                .Collection(c => c.Classes)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassReadDto>(course.Classes.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassReadDto> GetClassAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }

        public async Task<ClassReadDto> CreateClassAsync(string classCourseId, ClassCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);

            await ValidateDuplicationAsync<Class>(dto.Id, dto.Semester, dto.Year, classCourseId);

            var inputClass = new Class 
            { 
                Id = dto.Id, 
                Semester = (int) dto.Semester, 
                Year = (int) dto.Year, 
                CourseId = classCourseId 
            };

            await _context.AddAsync(inputClass);
            await _context.SaveChangesAsync();

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }

        public async Task<ClassReadDto> DeleteClassAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            _context.Remove(inputClass);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }
        #endregion

        #region StudyMaterials
        public async Task<List<StudyMaterialReadDto>> GetStudyMaterialsAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.StudyMaterials)
                .Query()
                .Include(sm => sm.StudyMaterialAttachments)
                .LoadAsync();

            return await _mapper.ProjectTo<StudyMaterialReadDto>(
                inputClass.StudyMaterials
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<StudyMaterialReadDto> GetStudyMaterialAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(studyMaterial)
                .Collection(sm => sm.StudyMaterialAttachments)
                .LoadAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }

        public async Task UpdateStudyMaterialAsync(
            string classCourseId, string classId, int classSemester,
            int classYear, int studyMaterialId, StudyMaterialCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _mapper.Map(dto, studyMaterial);

            _context.Update(studyMaterial);
            await _context.SaveChangesAsync();
        }

        public async Task<StudyMaterialReadDto> CreateStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, StudyMaterialCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var studyMaterial = new StudyMaterial
            {
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                Name = dto.Name,
                Description = dto.Description,
                Week = dto.Week
            };

            await _context.AddAsync(studyMaterial);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }

        public async Task<StudyMaterialReadDto> DeleteStudyMaterialAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(studyMaterial)
                .Reference(sm => sm.Class)
                .Query()
                .Include(c => c.Course)
                .LoadAsync();

            _context.Remove(studyMaterial);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }
        #endregion
    }
}
