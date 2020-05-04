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
using Identity.Api.Services;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees()
        {
            return await _employeeService.GetAllAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetEmployee(int id)
        {
            return await _employeeService.GetAsync(id);
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeUpdateDto dto)
        {
            await _employeeService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> PostEmployee(EmployeeCreateDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> DeleteEmployee(int id)
        {
            return await _employeeService.DeleteAsync(id);
        }

        // Roles

        [HttpGet("{id}/roles")]
        public async Task<ActionResult<IEnumerable<RoleReadDto>>> GetRoles(int id)
        {
            return await _employeeService.GetRolesAsync(id);
        }

        [HttpPost("{id}/roles")]
        public async Task<ActionResult<RoleReadDto>> AddRole(int id, [FromBody, Required] string roleName)
        {
            var role = await _employeeService.AddRoleAsync(id, roleName);

            return CreatedAtAction(nameof(GetRoles), new { id }, role);
        }

        [HttpDelete("{employeeId}/roles/{roleName}")]
        public async Task<ActionResult<RoleReadDto>> RemoveRole(int employeeId, string roleName)
        {
            return await _employeeService.RemoveRoleAsync(employeeId, roleName);
        }

        // Programs

        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetPrograms(int id)
        {
            return await _employeeService.GetProgramsAsync(id);
        }

        [HttpPost("{id}/programs")]
        public async Task<ActionResult<ProgramReadDto>> AddProgram(int id, [FromBody, Required] string programId)
        {
            var program = await _employeeService.AddProgramAsync(id, programId);

            return CreatedAtAction(nameof(GetPrograms), new { id }, program);
        }

        [HttpDelete("{employeeId}/programs/{programId}")]
        public async Task<ActionResult<ProgramReadDto>> RemoveProgram(int employeeId, string programId)
        {
            return await _employeeService.RemoveProgramAsync(employeeId, programId);
        }

    }
}
