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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudentsController(IdentityContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetStudents()
        {
            return await _mapper.ProjectTo<StudentReadDto>(_userManager.Users.OfType<Student>()).ToListAsync();
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
            // Validate required fields
            if (dto.FirstName == null
                || dto.LastName == null
                || dto.Initials == null
                || dto.ProgramId == null)
                return BadRequest();

            if (!(await _userManager.FindByIdAsync(id.ToString()) is Student student)
                || await _context.FindAsync<Models.Program>(dto.ProgramId) == null)
                return NotFound();

            _ = _mapper.Map(dto, student);

            await _userManager.UpdateAsync(student);

            return NoContent();
        }

        // POST: api/Students
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentCreateDto dto)
        {
            if (dto.FirstName == null
                || dto.LastName == null
                || dto.Initials == null
                || dto.Email == null
                || dto.DateOfBirth == null
                || dto.Nationality == null
                || dto.ProgramId == null)
                return BadRequest();

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Conflict();

            if (await _context.FindAsync<Models.Program>(dto.ProgramId) == null)
                return NotFound();

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

    }
}
