using Microsoft.AspNetCore.Mvc;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    }
}
