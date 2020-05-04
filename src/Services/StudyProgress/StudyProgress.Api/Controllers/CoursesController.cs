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

        // GET: api/Courses/prc1
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse(string id)
        {
            return await _courseService.GetAsync(id);
        }

        // PUT: api/Courses/prc1
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCourse(string id, CourseCreateUpdateDto dto)
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

        // DELETE: api/Courses/prc1
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseReadDto>> DeleteCourse(string id)
        {
            return await _courseService.DeleteAsync(id);
        }
        #endregion

        #region Programs
        // GET: api/Courses/prc1/Programs
        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetPrograms(string id)
        {
            return await _courseService.GetProgramsAsync(id);
        }

        // POST: api/Courses/prc1/Programs
        [HttpPost("{id}/programs")]
        public async Task<ActionResult<ProgramReadDto>> AddProgram(string id, [FromBody, Required] string programId)
        {
            var program = await _courseService.AddProgramAsync(id, programId);

            return CreatedAtAction(nameof(GetPrograms), new { id }, program);
        }

        // DELETE: api/Courses/prc1/Programs/5
        [HttpDelete("{id}/programs/{programId}")]
        public async Task<ActionResult<ProgramReadDto>> RemoveProgram(string id, string programId)
        {
            return await _courseService.RemoveProgramAsync(id, programId);
        }
        #endregion

        #region Requirements
        // GET: api/Courses/prc1/Requirements
        [HttpGet("{id}/requirements")]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetRequirements(string id)
        {
            return await _courseService.GetRequirementsAsync(id);
        }

        // POST: api/Courses/prc1/Requirements
        [HttpPost("{id}/requirements")]
        public async Task<ActionResult<CourseReadDto>> AddRequirement(string id, [FromBody, Required] string requiredCourseId)
        {
            var requiredCourse = await _courseService.AddRequirementAsync(id, requiredCourseId);

            return CreatedAtAction(nameof(GetRequirements), new { id }, requiredCourse);
        }

        // DELETE: api/Courses/prc1/Requirements/5
        [HttpDelete("{id}/requirements/{requiredCourseId}")]
        public async Task<ActionResult<CourseReadDto>> RemoveRequirement(string id, string requiredCourseId)
        {
            return await _courseService.RemoveRequirementAsync(id, requiredCourseId);
        }
        #endregion

        #region Enrollments
        // GET: api/Courses/prc1/Classes/S-71/1/3/Enrollments
        [HttpGet("{id}/classes/{classId}/{classSemester}/{classYear}/enrollments")]
        public async Task<ActionResult<IEnumerable<ClassEnrollmentReadDto>>> GetEnrollments(string id, string classId, int classSemester, int classYear)
        {
            return await _courseService.GetEnrollmentsAsync(id, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes/S-71/1/3/Enrollments
        [HttpPost("{id}/classes/{classId}/{classSemester}/{classYear}/enrollments")]
        public async Task<ActionResult<ClassEnrollmentReadDto>> AddEnrollment(string id, string classId, int classSemester, int classYear, [FromBody, Required] int studentId)
        {
            var enrollment = await _courseService.AddEnrollmentAsync(id, classId, classSemester, classYear, studentId);

            return CreatedAtAction(nameof(GetEnrollments), new { id, classId, classSemester, classYear }, enrollment);
        }

        // DELETE: api/Courses/prc1/Classes/S-71/1/3/Enrollments/1234567
        [HttpDelete("{id}/classes/{classId}/{classSemester}/{classYear}/enrollments/{studentId}")]
        public async Task<ActionResult<ClassEnrollmentReadDto>> RemoveEnrollment(string id, string classId, int classSemester, int classYear, int studentId)
        {
            return await _courseService.RemoveEnrollmentAsync(id, classId, classSemester, classYear, studentId);
        }
        #endregion
    }
}
