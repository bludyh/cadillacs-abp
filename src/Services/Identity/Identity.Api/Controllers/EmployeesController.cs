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
using System.ComponentModel.DataAnnotations;
using Bogus.DataSets;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public EmployeesController(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees()
        {
            return await _mapper.ProjectTo<EmployeeReadDto>(_userManager.Users.OfType<Employee>()).ToListAsyncFallback();
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
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            if ((dto.SchoolId != null && await _context.FindAsync<School>(dto.SchoolId) == null)
                || (dto.RoomId != null && dto.BuildingId != null && _context.Find<Room>(dto.RoomId, dto.BuildingId) == null))
                return UnprocessableEntity();

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
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return Conflict();

            if ((dto.SchoolId != null && await _context.FindAsync<School>(dto.SchoolId) == null)
                || (dto.RoomId != null && dto.BuildingId != null && _context.Find<Room>(dto.RoomId, dto.BuildingId) == null))
                return UnprocessableEntity();

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

        // Roles

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            return await _mapper.ProjectTo<RoleDto>((await _userManager.GetRolesAsync(employee)).AsQueryable()).ToListAsyncFallback();
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<IdentityUserRole<int>>> AddRole(int id, [FromBody, Required] string roleName)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            if (!(await _roleManager.FindByNameAsync(roleName) is IdentityRole<int> role))
                return UnprocessableEntity();

            if (await _userManager.IsInRoleAsync(employee, roleName))
                return Conflict();

            await _userManager.AddToRoleAsync(employee, roleName);

            return CreatedAtAction(nameof(GetRoles), new { id = employee.Id }, new IdentityUserRole<int> { RoleId = role.Id, UserId = employee.Id });
        }

        [HttpDelete("{employeeId}/roles/{roleName}")]
        public async Task<ActionResult<IdentityUserRole<int>>> RemoveRole(int employeeId, string roleName)
        {
            if (!(await _userManager.FindByIdAsync(employeeId.ToString()) is Employee employee)
                || !(await _roleManager.FindByNameAsync(roleName) is IdentityRole<int> role)
                || !await _userManager.IsInRoleAsync(employee, roleName))
                return NotFound();

            await _userManager.RemoveFromRoleAsync(employee, roleName);

            return new IdentityUserRole<int> { RoleId = role.Id, UserId = employee.Id };
        }

        // Programs

        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramDto>>> GetPrograms(int id)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            await _context.Entry(employee)
                .Collection(e => e.EmployeePrograms)
                .Query()
                .Include(ep => ep.Program)
                .ThenInclude(p => p.School)
                .LoadAsync();

            return await _mapper.ProjectTo<ProgramDto>(employee.EmployeePrograms.Select(ep => ep.Program).AsQueryable()).ToListAsyncFallback();
        }

        [HttpPost("{id}/programs")]
        public async Task<ActionResult<EmployeeProgram>> AddProgram(int id, [FromBody, Required] string programId)
        {
            if (!(await _userManager.FindByIdAsync(id.ToString()) is Employee employee))
                return NotFound();

            if (!(await _context.FindAsync<Models.Program>(programId) is Models.Program program))
                return UnprocessableEntity();

            if (await _context.FindAsync<EmployeeProgram>(id, programId) != null)
                return Conflict();

            var ep = new EmployeeProgram { Employee = employee, Program = program };

            await _context.AddAsync(ep);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrograms), new { id = ep.EmployeeId }, ep);
        }

        [HttpDelete("{employeeId}/programs/{programId}")]
        public async Task<ActionResult<EmployeeProgram>> RemoveProgram(int employeeId, string programId)
        {
            if (!(await _context.FindAsync<EmployeeProgram>(employeeId, programId) is EmployeeProgram ep))
                return NotFound();

            _context.Remove(ep);
            await _context.SaveChangesAsync();

            return ep;
        }

    }
}
