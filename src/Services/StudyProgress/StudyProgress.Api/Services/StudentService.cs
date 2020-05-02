using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
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
    public interface IStudentService
    {
        public Task<List<StudentEnrollmentReadDto>> GetEnrollmentsAsync(int studentId);
        public Task<StudentEnrollmentReadDto> AddEnrollmentAsync(int studentId, StudentEnrollmentCreateDto dto);
        //public Task<StudentEnrollmentReadDto> RemoveEnrollmentAsync(int studentId, int teacherId, string mentorType);


    }

    public class StudentService : ServiceBase, IStudentService
    {
        private readonly IMapper _mapper;

        public StudentService(StudyProgressContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }


        public async Task<List<StudentEnrollmentReadDto>> GetEnrollmentsAsync(int studentId)
        {
            var student = await ValidateExistenceAsync<Student>(studentId);

            await _context.Entry(student)
                .Collection(s => s.Enrollments)
                .Query()
                .Include(m => m.Class).ThenInclude(t => t.Course)
                .LoadAsync();

            return await _mapper.ProjectTo<StudentEnrollmentReadDto>(student.Enrollments.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<StudentEnrollmentReadDto> AddEnrollmentAsync(int studentId, StudentEnrollmentCreateDto dto)
        {
            //Validations
            var student = await ValidateExistenceAsync<Student>(studentId);
            var inputClass = await ValidateForeignKeyAsync<Class>(dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId);

            await ValidateDuplicationAsync<Enrollment>(studentId, dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId);

            //var enrollment = new Enrollment()
            //                    {
            //                        StudentId = studentId,
            //                        ClassId = dto.ClassId,
            //                        ClassSemester = (int) dto.ClassSemester,
            //                        ClassYear = (int) dto.ClassYear,
            //                        ClassCourseId = (int) dto.ClassCourseId
            //                    };

            //Create the new object
            var enrollment = _mapper.Map<Enrollment>(dto);
            enrollment.StudentId = studentId;

            //Add the new obj to the db
            await _context.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            //Links the relevant properties and subclasses
            await _context.Entry(enrollment).Reference(e => e.Student).LoadAsync();
            await _context.Entry(enrollment).Reference(e => e.Class).Query().Include(c => c.Course).LoadAsync();


            //Returns the readDto
            return _mapper.Map<StudentEnrollmentReadDto>(enrollment);
        }
    }
}
