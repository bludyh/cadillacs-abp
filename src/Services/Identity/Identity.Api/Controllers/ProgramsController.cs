using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models;
using Identity.Api.Services;
using Identity.Api.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Controllers
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

        [HttpGet("{programId}/employees")]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees(string programId)
        {
            return await _programService.GetEmployeesAsync(programId);
        }

        [HttpPost("{programId}/employees")]
        public async Task<ActionResult<EmployeeReadDto>> AddEmployee(string programId, [FromBody, Required] int employeeId)
        {
            var employee = await _programService.AddEmployeeAsync(programId, employeeId);

            return CreatedAtAction(nameof(GetEmployees), new { programId }, employee);
        }

        [HttpDelete("{programId}/employees/{employeeId}")]
        public async Task<ActionResult<EmployeeReadDto>> DeleteProgram(string programId, int employeeId)
        {
            return await _programService.RemoveEmployeeAsync(programId, employeeId);
        }

    }
}
