using AutoMapper;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Models;
using Infrastructure.Common;
using Infrastructure.Common.Exceptions;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface IEmployeeService
    {
        public Task<List<EmployeeReadDto>> GetAllAsync();
        public Task<EmployeeReadDto> GetAsync(int employeeId);
        public Task UpdateAsync(int employeeId, EmployeeUpdateDto dto);
        public Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto);
        public Task<EmployeeReadDto> DeleteAsync(int employeeId);
        public Task<List<RoleDto>> GetRolesAsync(int employeeId); 
        public Task<RoleDto> AddRoleAsync(int employeeId, string roleName); 
        public Task<RoleDto> RemoveRoleAsync(int employeeId, string roleName);
        public Task<List<ProgramDto>> GetProgramsAsync(int employeeId);
        public Task<ProgramDto> AddProgramAsync(int employeeId, string programId);
        public Task<ProgramDto> RemoveProgramAsync(int employeeId, string programId);
    }

    public class EmployeeService : ServiceBase, IEmployeeService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public EmployeeService(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
            : base(context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<EmployeeReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<EmployeeReadDto>(_context.Set<Employee>()).ToListAsyncFallback();
        }

        public async Task<EmployeeReadDto> GetAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

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

        public async Task UpdateAsync(int employeeId, EmployeeUpdateDto dto)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            await ValidateForeignKeyAsync<Room>(dto.RoomId, dto.BuildingId);

            _mapper.Map(dto, employee);

            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto)
        {
            Validate(
                condition: await _userManager.FindByEmailAsync(dto.Email) is Employee,
                message: $"Employee with email '{dto.Email}' already exists.",
                status: StatusCodes.Status409Conflict);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            await ValidateForeignKeyAsync<Room>(dto.RoomId, dto.BuildingId);

            var employee = _mapper.Map<Employee>(dto);
            employee.UserName = await ((IdentityContext)_context).GetNextPcn();

            await _userManager.CreateAsync(employee);
            await _userManager.AddToRoleAsync(employee, "Employee");

            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<EmployeeReadDto> DeleteAsync(int employeeId) 
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            await _context.Entry(employee)
                .Reference(e => e.School)
                .LoadAsync();

            await _context.Entry(employee)
                .Reference(e => e.Room)
                .Query()
                .Include(r => r.Building)
                .LoadAsync();

            _context.Remove(employee);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<List<RoleDto>> GetRolesAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            return await _mapper.ProjectTo<RoleDto>((await _userManager.GetRolesAsync(employee)).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<RoleDto> AddRoleAsync(int employeeId, string roleName)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' does not exist.",
                status: StatusCodes.Status422UnprocessableEntity);

            Validate(
                condition: await _userManager.IsInRoleAsync(employee, roleName),
                message: $"Employee '{employeeId}' already has Role '{roleName}'.",
                status: StatusCodes.Status409Conflict);

            await _userManager.AddToRoleAsync(employee, roleName);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> RemoveRoleAsync(int employeeId, string roleName)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' does not exist.",
                status: StatusCodes.Status404NotFound);

            Validate(
                condition: !await _userManager.IsInRoleAsync(employee, roleName),
                message: $"Employee '{employeeId}' does not have Role '{roleName}'.",
                status: StatusCodes.Status404NotFound);

            await _userManager.RemoveFromRoleAsync(employee, roleName);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<List<ProgramDto>> GetProgramsAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<Employee>(employeeId);

            await _context.Entry(employee)
                .Collection(e => e.EmployeePrograms)
                .Query()
                .Include(ep => ep.Program).ThenInclude(p => p.School)
                .LoadAsync();

            return await _mapper.ProjectTo<ProgramDto>(employee.EmployeePrograms.Select(ep => ep.Program).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ProgramDto> AddProgramAsync(int employeeId, string programId)
        {
            await ValidateExistenceAsync<Employee>(employeeId);

            await ValidateForeignKeyAsync<Models.Program>(programId);

            await ValidateDuplicationAsync<EmployeeProgram>(employeeId, programId);

            var ep = new EmployeeProgram { EmployeeId = employeeId, ProgramId = programId };

            await _context.AddAsync(ep);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramDto>(program);
        }

        public async Task<ProgramDto> RemoveProgramAsync(int employeeId, string programId)
        {
            var ep = await _context.FindAsync<EmployeeProgram>(employeeId, programId);
            Validate(
                condition: !(ep is EmployeeProgram),
                message: $"Employee '{employeeId}' is not in Program '{programId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(ep);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramDto>(program);
        }

    }
}
