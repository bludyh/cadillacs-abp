using AutoMapper;
using Infrastructure.Common.Services;
using Course.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Dtos;
using Course.Common.Models;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Course.Api.Services
{
    public interface ITeacherService
    {
        public Task<List<LecturerReadDto>> GetLecturersAsync(int teacherId);
        public Task<LecturerReadDto> AddLecturerAsync(int teacherId, TeacherLecturerCreateDto dto);
        public Task<LecturerReadDto> RemoveLecturerAsync(int teacherId, string classId, int classSemester, int classYear, string classCourseId);
    }

    public class TeacherService<T> : ServiceBase, ITeacherService where T : Common.Models.Course
    {
        private readonly IMapper _mapper;

        public TeacherService(CourseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<LecturerReadDto>> GetLecturersAsync(int teacherId)
        {
            var teacher = await ValidateExistenceAsync<Teacher>(teacherId);

            await _context.Entry(teacher)
                .Collection(s => s.Lecturers)
                .Query()
                .Include(l => l.Class)
                .ThenInclude(c => c.Course)
                .LoadAsync();

            return await _mapper.ProjectTo<LecturerReadDto>(teacher.Lecturers.AsQueryable()).ToListAsyncFallback();

        }

        public async Task<LecturerReadDto> AddLecturerAsync(int teacherId, TeacherLecturerCreateDto dto)
        {
            await ValidateExistenceAsync<Common.Models.Course>(dto.ClassCourseId);
            await ValidateExistenceAsync<Class>(dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId);
            await ValidateExistenceAsync<Teacher>(teacherId);
            await ValidateForeignKeyAsync<Common.Models.Course>(dto.ClassCourseId);
            await ValidateForeignKeyAsync<Class>(dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId);
            await ValidateForeignKeyAsync<Teacher>(teacherId);

            await ValidateDuplicationAsync<Lecturer>(teacherId, dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId);

            var lecturer = new Lecturer 
            { 
                TeacherId = teacherId,
                ClassId = dto.ClassId,
                ClassSemester = (int)dto.ClassSemester,
                ClassYear = (int)dto.ClassYear,
                ClassCourseId = dto.ClassCourseId
            };

            await _context.AddAsync(lecturer);
            await _context.SaveChangesAsync();

            return _mapper.Map<LecturerReadDto>(lecturer);
        }

        public async Task<LecturerReadDto> RemoveLecturerAsync(int teacherId, string classId, int classSemester, int classYear, string classCourseId)
        {
            await ValidateExistenceAsync<Common.Models.Course>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Teacher>(teacherId);
            var lecturer = await ValidateExistenceAsync<Lecturer>(
                teacherId, classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Common.Models.Course>(classCourseId);
            await ValidateForeignKeyAsync<Teacher>(teacherId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _context.Remove(lecturer);
            await _context.SaveChangesAsync();

            return _mapper.Map<LecturerReadDto>(lecturer);
        }
    }
}
