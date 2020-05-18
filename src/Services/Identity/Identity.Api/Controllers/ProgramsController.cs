using Identity.Api.Dtos;
using Identity.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetEmployees([FromRoute] string programId)
        {
            return await _programService.GetEmployeesAsync(programId);
        }

        [HttpPost("{programId}/employees")]
        public async Task<ActionResult<EmployeeReadDto>> AddEmployee([FromRoute] string programId, [FromBody, Required] int employeeId)
        {
            var employee = await _programService.AddEmployeeAsync(programId, employeeId);

            return CreatedAtAction(nameof(GetEmployees), new { programId }, employee);
        }

        [HttpDelete("{programId}/employees/{employeeId}")]
        public async Task<ActionResult<EmployeeReadDto>> DeleteProgram([FromRoute] string programId, [FromRoute] int employeeId)
        {
            return await _programService.RemoveEmployeeAsync(programId, employeeId);
        }

    }
}
