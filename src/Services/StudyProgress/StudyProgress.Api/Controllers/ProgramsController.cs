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
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramsController(IProgramService programService)
        {
            _programService = programService;
        }

        #region Programs
        // GET: api/Programs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetPrograms()
        {
            return await _programService.GetAllAsync();
        }

        // GET: api/Programs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramReadDto>> GetProgram(int id)
        {
            return await _programService.GetAsync(id);
        }

        // PUT: api/Programs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, ProgramUpdateDto dto)
        {
            await _programService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Programs
        [HttpPost]
        public async Task<ActionResult<ProgramReadDto>> PostProgram(ProgramCreateDto dto)
        {
            var program = await _programService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetProgram), new { id = program.Id }, program);
        }

        // DELETE: api/Programs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProgramReadDto>> DeleteProgram(int id)
        {
            return await _programService.DeleteAsync(id);
        }
        #endregion

        #region Courses
        // GET: api/Programs/5/Courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses(int id)
        {
            return await _programService.GetCoursesAsync(id);
        }

        // POST: api/Programs/5/Courses
        [HttpPost("{id}/courses")]
        public async Task<ActionResult<CourseReadDto>> AddCourse(int id, [FromBody, Required] int courseId)
        {
            var course = await _programService.AddCourseAsync(id, courseId);

            return CreatedAtAction(nameof(GetCourses), new { id }, course);
        }

        // DELETE: api/Programs/5/Courses/5
        [HttpDelete("{id}/courses/{courseId}")]
        public async Task<ActionResult<CourseReadDto>> RemoveProgram(int id, int courseId)
        {
            return await _programService.RemoveCourseAsync(id, courseId);
        }
        #endregion
    }
}
