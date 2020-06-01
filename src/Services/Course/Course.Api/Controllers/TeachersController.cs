using Course.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Course.Api.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Course.Api.Services.TeachersService;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;


        [HttpGet("{id}/lecturers")]
        public async Task<ActionResult<IEnumerable<LecturerReadDto>>> GetLecturers([FromRoute] int id)
        {
            return await _teacherService.GetLecturersAsync(id);
        }

        [HttpPost("{id}/lecturers")]
        public async Task<ActionResult<LecturerReadDto>> AddLecturer([FromRoute] int id, [FromBody] LecturerCreateDto dto)
        {
            var lecturer = await _teacherService.AddLecturerAsync(id, dto);

            return CreatedAtAction(nameof(GetLecturers), new { id }, lecturer);
        }

        [HttpDelete("{id}/lecturers")]
        public async Task<ActionResult<LecturerReadDto>> RemoveLecturer([FromRoute] int id)
        {
            return await _teacherService.RemoveLecturerAsync(id);
        }
    }
}
