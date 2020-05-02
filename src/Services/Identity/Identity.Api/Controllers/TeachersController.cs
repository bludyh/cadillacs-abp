﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Identity.Api.Dtos;
using Infrastructure.Common;
using Identity.Api.Services;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetTeachers()
        {
            return await _teacherService.GetAllAsync();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetTeacher(int id)
        {
            return await _teacherService.GetAsync(id);
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, EmployeeUpdateDto dto)
        {
            await _teacherService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> PostTeacher(EmployeeCreateDto dto)
        {
            var teacher = await _teacherService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> DeleteTeacher(int id)
        {
            return await _teacherService.DeleteAsync(id);
        }

        // Roles

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles(int id)
        {
            return await _teacherService.GetRolesAsync(id);
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<RoleDto>> AddRole(int id, [FromBody, Required] string roleName)
        {
            var role = await _teacherService.AddRoleAsync(id, roleName);

            return CreatedAtAction(nameof(GetRoles), new { id }, role);
        }

        [HttpDelete("{teacherId}/roles/{roleName}")]
        public async Task<ActionResult<RoleDto>> RemoveRole(int teacherId, string roleName)
        {
            return await _teacherService.RemoveRoleAsync(teacherId, roleName);
        }

        // Programs

        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramDto>>> GetPrograms(int id)
        {
            return await _teacherService.GetProgramsAsync(id);
        }

        [HttpPost("{id}/programs")]
        public async Task<ActionResult<ProgramDto>> AddProgram(int id, [FromBody, Required] string programId)
        {
            var program = await _teacherService.AddProgramAsync(id, programId);

            return CreatedAtAction(nameof(GetPrograms), new { id }, program);
        }

        [HttpDelete("{teacherId}/programs/{programId}")]
        public async Task<ActionResult<ProgramDto>> RemoveProgram(int teacherId, string programId)
        {
            return await _teacherService.RemoveProgramAsync(teacherId, programId);
        }

        [HttpGet("{id}/mentors")]
        public async Task<ActionResult<IEnumerable<TeacherMentorReadDto>>> GetMentors(int id)
        {
            return await _teacherService.GetMentorsAsync(id);
        }

        [HttpPost("{id}/mentors")]
        public async Task<ActionResult<TeacherMentorReadDto>> AddMentor(int id, TeacherMentorCreateDto dto)
        {
            var mentor = await _teacherService.AddMentorAsync(id, dto);

            return CreatedAtAction(nameof(GetMentors), new { id }, mentor);
        }

        [HttpDelete("{id}/mentors")]
        public async Task<ActionResult<TeacherMentorReadDto>> RemoveMentor(int id, [FromQuery, Required] int studentId, [FromQuery, Required] string mentorType)
        {
            return await _teacherService.RemoveMentorAsync(id, studentId, mentorType);
        }

    }
}
