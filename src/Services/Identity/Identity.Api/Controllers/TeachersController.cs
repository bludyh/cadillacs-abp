using System;
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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public TeachersController(IdentityContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetTeachers()
        {
            return await _mapper.ProjectTo<EmployeeReadDto>(_userManager.Users.OfType<Teacher>()).ToListAsyncFallback();
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetTeacher(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Teacher teacher))
                return NotFound();

            await _context.Entry(teacher)
                .Reference(e => e.School)
                .LoadAsync();

            await _context.Entry(teacher)
                .Reference(e => e.Room)
                .Query()
                .Include(r => r.Building)
                .LoadAsync();

            return _mapper.Map<EmployeeReadDto>(teacher);
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, EmployeeUpdateDto dto)
        {
            // Check if keys are valid
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Teacher teacher)
                || (dto.SchoolId != null && await _context.FindAsync<School>(dto.SchoolId) == null)
                || (dto.RoomId != null && dto.BuildingId != null && await _context.FindAsync<Room>(dto.RoomId, dto.BuildingId) == null))
                return NotFound();

            _mapper.Map(dto, teacher);

            await _userManager.UpdateAsync(teacher);

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(EmployeeCreateDto dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Conflict();

            // Check if foreign keys are valid
            if ((dto.SchoolId != null) && await _context.FindAsync<School>(dto.SchoolId) == null
                || (dto.RoomId != null && dto.BuildingId != null && await _context.FindAsync<Room>(dto.RoomId, dto.BuildingId) == null))
                return NotFound();

            var teacher = _mapper.Map<Teacher>(dto);
            teacher.UserName = await _context.GetNextPcn();

            await _userManager.CreateAsync(teacher);
            await _userManager.AddToRoleAsync(teacher, "Employee");
            await _userManager.AddToRoleAsync(teacher, "Teacher");

            return CreatedAtAction("GetEmployee", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> DeleteTeacher(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Teacher teacher))
                return NotFound();

            await _userManager.DeleteAsync(teacher);

            return teacher;
        }

    }
}
