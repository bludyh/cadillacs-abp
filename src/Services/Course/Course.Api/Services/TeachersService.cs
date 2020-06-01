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
    public class TeachersService
    {
        public interface ITeacherService
        {
            public Task<List<LecturerReadDto>> GetLecturersAsync(int teacherId);
            public Task<LecturerReadDto> AddLecturerAsync(int teacherId, LecturerCreateDto dto);
            public Task<LecturerReadDto> RemoveLecturerAsync(int teacherId);
        }

        public class TeacherService<T> : ServiceBase, ITeacherService where T : Common.Models.Course
        {
            private readonly IMapper _mapper;

            public TeacherService(CourseContext context, IMapper mapper) : base(context)
            {
                _mapper = mapper;
            }

            public async Task<LecturerReadDto> AddLecturerAsync(int teacherId, LecturerCreateDto dto)
            {
                var teacher = await ValidateForeignKeyAsync<Teacher>(dto.TeacherId);
                await ValidateDuplicationAsync<Lecturer>(dto.TeacherId);


                var lecturer = new Lecturer { TeacherId = teacherId};

                await _context.AddAsync(lecturer);
                await _context.SaveChangesAsync();

                await _context.Entry(teacher)
                    .Collection(s => s.Lecturers)
                    .Query()
                    .Include(m => m.Teacher)
                    .LoadAsync();

                return _mapper.Map<LecturerReadDto>(lecturer);
            }

            public async Task<List<LecturerReadDto>> GetLecturersAsync(int teacherId)
            {
                var teacher = await ValidateExistenceAsync<Teacher>(teacherId);

                await _context.Entry(teacher)
                    .Collection(s => s.Lecturers)
                    .Query()
                    .Include(m => m.Teacher)
                    .LoadAsync();

                return await _mapper.ProjectTo<LecturerReadDto>(teacher.Lecturers.AsQueryable()).ToListAsyncFallback();

            }

            public async Task<LecturerReadDto> RemoveLecturerAsync(int teacherId)
            {
                var teacher = await ValidateExistenceAsync<Teacher>(teacherId); //or Lecturer?

                var lecturer = await _context.FindAsync<Lecturer>(teacherId);

                await _context.Entry(teacher)
                    .Collection(s => s.Lecturers)
                    .Query()
                    .Include(m => m.Teacher)
                    .LoadAsync();

                _context.Remove(lecturer);
                await _context.SaveChangesAsync();

                return _mapper.Map<LecturerReadDto>(lecturer);
            }
        }
    }
}
