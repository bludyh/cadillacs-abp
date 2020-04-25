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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public EmployeesController(IdentityContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees()
        {
            return await _mapper.ProjectTo<EmployeeReadDto>(_userManager.Users.OfType<Employee>()).ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployee(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            // Explicitly load School and Room properties
            await _context.Entry(employee)
                .Reference(e => e.School)
                .LoadAsync();

            await _context.Entry(employee)
                .Reference(e => e.Room)
                .Query()
                .Include(r => r.Building)
                .LoadAsync();

            return _mapper.Map<EmployeeReadDto>(employee);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto dto)
        {
            if (dto.FirstName == null
                || dto.LastName == null
                || dto.Initials == null
                || (dto.RoomId == null && dto.BuildingId != null)
                || (dto.RoomId != null && dto.BuildingId == null))
                return BadRequest();

            // Check if keys are valid
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee)
                || (dto.SchoolId != null && await _context.FindAsync<School>(dto.SchoolId) == null)
                || (dto.RoomId != null && dto.BuildingId != null && await _context.FindAsync<Room>(dto.RoomId, dto.BuildingId) == null))
                return NotFound();

            _mapper.Map(dto, employee);

            await _userManager.UpdateAsync(employee);

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeCreateDto dto)
        {
            if (dto.FirstName == null
                || dto.LastName == null
                || dto.Initials == null
                || dto.Email == null
                || (dto.RoomId == null && dto.BuildingId != null)
                || (dto.RoomId != null && dto.BuildingId == null))
                return BadRequest();

            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Conflict();

            // Check if foreign keys are valid
            if ((dto.SchoolId != null) && await _context.FindAsync<School>(dto.SchoolId) == null
                || (dto.RoomId != null && dto.BuildingId != null && await _context.FindAsync<Room>(dto.RoomId, dto.BuildingId) == null))
                return NotFound();

            var employee = _mapper.Map<Employee>(dto);
            employee.UserName = await _context.GetNextPcn();

            await _userManager.CreateAsync(employee);
            await _userManager.AddToRoleAsync(employee, "Employee");

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            await _userManager.DeleteAsync(employee);

            return employee;
        }

    }
}
