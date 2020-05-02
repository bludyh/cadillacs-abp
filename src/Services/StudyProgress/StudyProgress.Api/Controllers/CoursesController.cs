﻿using Microsoft.AspNetCore.Mvc;
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

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse(int id)
        {
            return await _courseService.GetAsync(id);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCourse(int id, CourseCreateUpdateDto dto)
        {
            await _courseService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> PostCourse(CourseCreateUpdateDto dto)
        {
            var course = await _courseService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseReadDto>> DeleteCourse(int id)
        {
            return await _courseService.DeleteAsync(id);
        }
        #endregion

        #region Programs
        // GET: api/Courses/5/Programs
        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetPrograms(int id)
        {
            return await _courseService.GetProgramsAsync(id);
        }

        // POST: api/Courses/5/Programs
        [HttpPost("{id}/programs")]
        public async Task<ActionResult<ProgramReadDto>> AddProgram(int id, [FromBody, Required] int programId)
        {
            var program = await _courseService.AddProgramAsync(id, programId);

            return CreatedAtAction(nameof(GetPrograms), new { id }, program);
        }

        // DELETE: api/Courses/5/Programs/5
        [HttpDelete("{id}/programs/{programId}")]
        public async Task<ActionResult<ProgramReadDto>> RemoveProgram(int id, int programId)
        {
            return await _courseService.RemoveProgramAsync(id, programId);
        }
        #endregion
    }
}
