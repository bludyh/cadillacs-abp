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

        #region Courses
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses()
        {
            return await _courseService.GetAllAsync();
        }

        // GET: api/Courses/prc1
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse([FromRoute] string id)
        {
            return await _courseService.GetAsync(id);
        }

        // PUT: api/Courses/prc1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] string id, [FromBody] CourseUpdateDto dto)
        {
            await _courseService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> PostCourse(CourseCreateDto dto)
        {
            var course = await _courseService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/prc1
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseReadDto>> DeleteCourse([FromRoute] string id)
        {
            return await _courseService.DeleteAsync(id);
        }
        #endregion

        #region Classes
        // GET: api/Courses/prc1/Classes
        [HttpGet("{classCourseId}/classes")]
        public async Task<ActionResult<IEnumerable<ClassReadDto>>> GetClasses([FromRoute] string classCourseId)
        {
            return await _courseService.GetClassesAsync(classCourseId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}")]
        public async Task<ActionResult<ClassReadDto>> GetClass(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetClassAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes
        [HttpPost("{classCourseId}/classes")]
        public async Task<ActionResult<ClassReadDto>> CreateClass(
            [FromRoute] string classCourseId,
            [FromBody] ClassCreateDto dto)
        {
            var inputClass = await _courseService.CreateClassAsync(classCourseId, dto);

            return CreatedAtAction(nameof(GetClasses), new { classCourseId }, inputClass);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}")]
        public async Task<ActionResult<ClassReadDto>> DeleteClass(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.DeleteClassAsync(classCourseId, classId, classSemester, classYear);
        }
        #endregion
    }
}