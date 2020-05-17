using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models;
using Identity.Api.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using Identity.Api.Services;

namespace Identity.Api.Controllers
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

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetStudents()
        {
            return await _studentService.GetAllAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudent([FromRoute] int id)
        {
            return await _studentService.GetAsync(id);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] StudentUpdateDto dto)
        {
            await _studentService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentReadDto>> PostStudent([FromBody] StudentCreateDto dto)
        {
            var student = await _studentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentReadDto>> DeleteStudent([FromRoute] int id)
        {
            return await _studentService.DeleteAsync(id);
        }

        // Roles

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetRoles([FromRoute] int id)
        {
            return await _studentService.GetRolesAsync(id);
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<RoleReadDto>> AddRole([FromRoute] int id, [FromBody, Required] string roleName)
        {
            var role = await _studentService.AddRoleAsync(id, roleName);

            return CreatedAtAction(nameof(GetRoles), new { id }, role);
        }

        [HttpDelete("{studentId}/roles/{roleName}")]
        public async Task<ActionResult<RoleReadDto>> RemoveRole([FromRoute] int studentId, [FromRoute] string roleName)
        {
            return await _studentService.RemoveRoleAsync(studentId, roleName);
        }

        // Mentors

        [HttpGet("{id}/mentors")]
        public async Task<ActionResult<IEnumerable<StudentMentorReadDto>>> GetMentors([FromRoute] int id)
        {
            return await _studentService.GetMentorsAsync(id);
        }

        [HttpPost("{id}/mentors")]
        public async Task<ActionResult<StudentMentorReadDto>> AddMentor([FromRoute] int id, [FromBody] StudentMentorCreateDto dto)
        {
            var mentor = await _studentService.AddMentorAsync(id, dto);

            return CreatedAtAction(nameof(GetMentors), new { id }, mentor);
        }

        [HttpDelete("{id}/mentors")]
        public async Task<ActionResult<StudentMentorReadDto>> RemoveMentor([FromRoute] int id, [FromQuery, Required] int teacherId, [FromQuery, Required] string mentorType)
        {
            return await _studentService.RemoveMentorAsync(id, teacherId, mentorType);
        }

    }
}
