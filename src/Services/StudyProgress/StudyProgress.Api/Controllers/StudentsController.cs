using Microsoft.AspNetCore.Mvc;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }



        // Enrollments

        [HttpGet("{id}/enrollments")]
        public async Task<ActionResult<IEnumerable<StudentEnrollmentReadDto>>> GetEnrollments(int id)
        {
            return await _studentService.GetEnrollmentsAsync(id);
        }

        [HttpPost("{id}/enrollments")]
        public async Task<ActionResult<StudentEnrollmentReadDto>> AddEnrollment(int id, StudentEnrollmentCreateDto dto)
        {
            var enrollment = await _studentService.AddEnrollmentAsync(id, dto);

            return CreatedAtAction(nameof(GetEnrollments), new { id }, enrollment);
        }

        //[HttpDelete("{id}/enrollments")]
        //public async Task<ActionResult<StudentMentorReadDto>> RemoveMentor(int id, [FromQuery, Required] int teacherId, [FromQuery, Required] string mentorType)
        //{
        //    return await _studentService.RemoveEnrollmentAsync(id, teacherId, mentorType);
        //}
    }
}
