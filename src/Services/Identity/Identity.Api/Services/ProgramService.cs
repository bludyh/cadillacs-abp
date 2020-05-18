using AutoMapper;
using Identity.Api.Dtos;
using Identity.Common.Data;
using Identity.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface IProgramService
    {
        public Task<List<EmployeeReadDto>> GetEmployeesAsync(string programId);
        public Task<EmployeeReadDto> AddEmployeeAsync(string programId, int employeeId);
        public Task<EmployeeReadDto> RemoveEmployeeAsync(string programId, int employeeId);
    }

    public class ProgramService : ServiceBase, IProgramService
    {
        private readonly IMapper _mapper;

        public ProgramService(IdentityContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<EmployeeReadDto> AddEmployeeAsync(string programId, int employeeId)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);
            var employee = await ValidateForeignKeyAsync<Employee>(employeeId);
            await ValidateDuplicationAsync<EmployeeProgram>(employeeId, programId);

            var ep = new EmployeeProgram { Employee = employee, Program = program };

            await _context.AddAsync(ep);
            await _context.SaveChangesAsync();

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

        public async Task<List<EmployeeReadDto>> GetEmployeesAsync(string programId)
        {
            var program = await ValidateExistenceAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Collection(p => p.EmployeePrograms)
                .Query()
                .Include(ep => ep.Employee).ThenInclude(e => e.School)
                .Include(ep => ep.Employee).ThenInclude(e => e.Room).ThenInclude(r => r.Building)
                .LoadAsync();

            return await _mapper.ProjectTo<EmployeeReadDto>(program.EmployeePrograms.Select(ep => ep.Employee).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<EmployeeReadDto> RemoveEmployeeAsync(string programId, int employeeId)
        {
            await ValidateExistenceAsync<Common.Models.Program>(programId);
            var employee = await ValidateForeignKeyAsync<Employee>(employeeId);
            var ep = await ValidateExistenceAsync<EmployeeProgram>(employeeId, programId);

            _context.Remove(ep);
            await _context.SaveChangesAsync();

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
    }
}
