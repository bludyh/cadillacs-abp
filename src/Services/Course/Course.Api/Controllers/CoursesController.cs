using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Dtos;
using Course.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses()
        {
            return await _courseService.GetAllAsync();
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] string id, [FromBody] CourseUpdateDto dto)
        {
            await _courseService.UpdateAsync(id, dto);

            return NoContent();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse([FromRoute] string id)
        {
            return await _courseService.GetAsync(id);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> PostCourse(CourseCreateDto dto)
        {
            var course = await _courseService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseReadDto>> DeleteCourse([FromRoute] string id)
        {
            return await _courseService.DeleteAsync(id);
        }
    }
}