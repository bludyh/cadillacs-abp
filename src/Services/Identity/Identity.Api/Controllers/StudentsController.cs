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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public StudentsController(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetStudents()
        {
            return await _mapper.ProjectTo<StudentReadDto>(_userManager.Users.OfType<Student>()).ToListAsyncFallback();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudent(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            // Explicitly load related resources
            await _context.Entry(student)
                .Reference(s => s.Program)
                .Query()
                .Include(p => p.School)
                .LoadAsync();

            return _mapper.Map<StudentReadDto>(student);
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, StudentUpdateDto dto)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            if (await _context.FindAsync<Models.Program>(dto.ProgramId) == null)
                return UnprocessableEntity();

            _mapper.Map(dto, student);

            await _userManager.UpdateAsync(student);

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Conflict();

            if (await _context.FindAsync<Models.Program>(dto.ProgramId) == null)
                return UnprocessableEntity();

            var student = _mapper.Map<Student>(dto);
            student.UserName = await _context.GetNextPcn();

            await _userManager.CreateAsync(student);
            await _userManager.AddToRoleAsync(student, "Student");

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            await _userManager.DeleteAsync(student);

            return student;
        }

        // Roles

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            return await _mapper.ProjectTo<RoleDto>((await _userManager.GetRolesAsync(student)).AsQueryable()).ToListAsyncFallback();
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<IdentityUserRole<int>>> AddRole(int id, [FromBody, Required] string roleName)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            if (!(await _roleManager.FindByNameAsync(roleName) is IdentityRole<int> role))
                return UnprocessableEntity();

            if (await _userManager.IsInRoleAsync(student, roleName))
                return Conflict();

            await _userManager.AddToRoleAsync(student, roleName);

            return CreatedAtAction(nameof(GetRoles), new { id = student.Id }, new IdentityUserRole<int> { RoleId = role.Id, UserId = student.Id });
        }

        [HttpDelete("{employeeId}/roles/{roleName}")]
        public async Task<ActionResult<IdentityUserRole<int>>> RemoveRole(int employeeId, string roleName)
        {
            if (!(await _userManager.FindByIdAsync(employeeId.ToString()) is Student student)
                || !(await _roleManager.FindByNameAsync(roleName) is IdentityRole<int> role)
                || !await _userManager.IsInRoleAsync(student, roleName))
                return NotFound();

            await _userManager.RemoveFromRoleAsync(student, roleName);

            return new IdentityUserRole<int> { RoleId = role.Id, UserId = student.Id };
        }

        // Mentors

        [HttpGet("{id}/mentors")]
        public async Task<ActionResult<IEnumerable<StudentMentorReadDto>>> GetMentors(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            await _context.Entry(student)
                .Collection(s => s.Mentors)
                .Query()
                .Include(m => m.Teacher).ThenInclude(t => t.School)
                .Include(m => m.Teacher).ThenInclude(t => t.Room).ThenInclude(r => r.Building)
                .LoadAsync();

            return await _mapper.ProjectTo<StudentMentorReadDto>(student.Mentors.AsQueryable()).ToListAsyncFallback();
        }

        [HttpPost("{id}/mentors")]
        public async Task<ActionResult<Mentor>> AddMentor(int id, StudentMentorCreateDto dto)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student))
                return NotFound();

            if (!(await _userManager.FindByIdAsync(dto.TeacherId.ToString()) is Teacher teacher)
                || !Enum.TryParse<MentorType>(dto.MentorType, true, out var type))
                return UnprocessableEntity();

            if (await _context.FindAsync<Mentor>(dto.TeacherId, id, type) != null)
                return Conflict();

            var mentor = new Mentor { Teacher = teacher, Student = student, MentorType = type };

            await _context.AddAsync(mentor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMentors), new { id = mentor.StudentId }, mentor);
        }

        [HttpDelete("{id}/mentors")]
        public async Task<ActionResult<Mentor>> RemoveMentor(int id, [FromQuery, Required] int teacherId, [FromQuery, Required] string mentorType)
        {
            if (!Enum.TryParse<MentorType>(mentorType, true, out var type)
                || !(await _context.FindAsync<Mentor>(teacherId, id, type) is Mentor mentor))
                return NotFound();

            _context.Remove(mentor);
            await _context.SaveChangesAsync();

            return mentor;
        }

    }
}
