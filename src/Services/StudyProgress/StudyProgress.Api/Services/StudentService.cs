using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using StudyProgress.Api.Dtos;
using StudyProgress.Common.Data;
using StudyProgress.Common.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Services
{
    public interface IStudentService
    {
        public Task<List<StudentEnrollmentReadDto>> GetEnrollmentsAsync(int studentId);
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

    }
}
