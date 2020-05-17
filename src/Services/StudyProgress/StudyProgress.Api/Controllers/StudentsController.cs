using Microsoft.AspNetCore.Mvc;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<ActionResult<IEnumerable<StudentEnrollmentReadDto>>> GetEnrollments([FromRoute] int id)
        {
            return await _studentService.GetEnrollmentsAsync(id);
        }

        [HttpPost("{id}/enrollments")]
        public async Task<ActionResult<StudentEnrollmentReadDto>> AddEnrollment([FromRoute] int id, [FromBody] StudentEnrollmentCreateDto dto)
        {
            var enrollment = await _studentService.AddEnrollmentAsync(id, dto);

            return CreatedAtAction(nameof(GetEnrollments), new { id }, enrollment);
        }

        [HttpDelete("{id}/enrollments")]
        public async Task<ActionResult<StudentEnrollmentReadDto>> RemoveEnrollment(
            [FromRoute] int id,
            [FromQuery, Required] string classId,
            [FromQuery, Required] int classSemester,
            [FromQuery, Required] int classYear,
            [FromQuery, Required] string classCourseId)
        {
            return await _studentService.RemoveEnrollmentAsync(id, classId, classSemester, classYear, classCourseId);
        }
    }
}
