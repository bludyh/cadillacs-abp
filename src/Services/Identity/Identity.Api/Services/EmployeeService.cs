using AutoMapper;
using Identity.Api.Dtos;
using Identity.Common.Data;
using Identity.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Events;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
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
        public Task<List<RoleReadDto>> GetRolesAsync(int employeeId);
        public Task<RoleReadDto> AddRoleAsync(int employeeId, string roleName);
        public Task<RoleReadDto> RemoveRoleAsync(int employeeId, string roleName);
        public Task<List<ProgramReadDto>> GetProgramsAsync(int employeeId);
        public Task<ProgramReadDto> AddProgramAsync(int employeeId, string programId);
        public Task<ProgramReadDto> RemoveProgramAsync(int employeeId, string programId);
    }

    public class EmployeeService<T> : ServiceBase, IEmployeeService where T : Employee
    {

        protected readonly IMapper _mapper;
        protected readonly UserManager<User> _userManager;
        protected readonly RoleManager<IdentityRole<int>> _roleManager;
        protected readonly IMessagePublisher _messagePublisher;

        public EmployeeService(IdentityContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IMessagePublisher messagePublisher)
            : base(context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _messagePublisher = messagePublisher;
        }

        public async Task<List<EmployeeReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<EmployeeReadDto>(_context.Set<T>()).ToListAsyncFallback();
        }

        public async Task<EmployeeReadDto> GetAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

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
            var employee = await ValidateExistenceAsync<T>(employeeId);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            await ValidateForeignKeyAsync<Room>(dto.RoomId, dto.BuildingId);

            _mapper.Map(dto, employee);

            _context.Update(employee);
            await _context.SaveChangesAsync();

            // Publish event
            var e = _mapper.Map<EmployeeUpdated>(employee);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");
        }

        public async Task<EmployeeReadDto> CreateAsync(EmployeeCreateDto dto)
        {
            Validate(
                condition: await _userManager.FindByEmailAsync(dto.Email) != null,
                message: $"{typeof(T).Name} with email '{dto.Email}' already exists.",
                status: StatusCodes.Status409Conflict);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            await ValidateForeignKeyAsync<Room>(dto.RoomId, dto.BuildingId);

            var employee = _mapper.Map<T>(dto);
            employee.UserName = await ((IdentityContext)_context).GetNextPcn();

            await _userManager.CreateAsync(employee);
            await _userManager.AddToRoleAsync(employee, "Employee");
            if (typeof(T).Name != "Employee")
                await _userManager.AddToRoleAsync(employee, typeof(T).Name);

            await _context.Entry(employee)
                .Reference(e => e.School)
                .LoadAsync();

            await _context.Entry(employee)
                .Reference(e => e.Room)
                .Query()
                .Include(r => r.Building)
                .LoadAsync();

            // Publish event
            var e = _mapper.Map<EmployeeCreated>(employee);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<EmployeeReadDto> DeleteAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

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

            // Publish event
            var e = _mapper.Map<EmployeeDeleted>(employee);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<EmployeeReadDto>(employee);
        }

        public async Task<List<RoleReadDto>> GetRolesAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

            return await _mapper.ProjectTo<RoleReadDto>((await _userManager.GetRolesAsync(employee)).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<RoleReadDto> AddRoleAsync(int employeeId, string roleName)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            Validate(
                condition: await _userManager.IsInRoleAsync(employee, roleName),
                message: $"{typeof(T).Name} '{employeeId}' already has Role '{roleName}'.",
                status: StatusCodes.Status409Conflict);

            await _userManager.AddToRoleAsync(employee, roleName);

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<RoleReadDto> RemoveRoleAsync(int employeeId, string roleName)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

            var role = await _roleManager.FindByNameAsync(roleName);
            Validate(
                condition: !(role is IdentityRole<int>),
                message: $"Role '{roleName}' is not valid.",
                status: StatusCodes.Status422UnprocessableEntity);

            Validate(
                condition: !await _userManager.IsInRoleAsync(employee, roleName),
                message: $"{typeof(T).Name} '{employeeId}' does not have Role '{roleName}'.",
                status: StatusCodes.Status404NotFound);

            await _userManager.RemoveFromRoleAsync(employee, roleName);

            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<List<ProgramReadDto>> GetProgramsAsync(int employeeId)
        {
            var employee = await ValidateExistenceAsync<T>(employeeId);

            await _context.Entry(employee)
                .Collection(e => e.EmployeePrograms)
                .Query()
                .Include(ep => ep.Program).ThenInclude(p => p.School)
                .LoadAsync();

            return await _mapper.ProjectTo<ProgramReadDto>(employee.EmployeePrograms.Select(ep => ep.Program).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ProgramReadDto> AddProgramAsync(int employeeId, string programId)
        {
            await ValidateExistenceAsync<T>(employeeId);

            await ValidateForeignKeyAsync<Common.Models.Program>(programId);

            await ValidateDuplicationAsync<EmployeeProgram>(employeeId, programId);

            var ep = new EmployeeProgram { EmployeeId = employeeId, ProgramId = programId };

            await _context.AddAsync(ep);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> RemoveProgramAsync(int employeeId, string programId)
        {
            await ValidateExistenceAsync<T>(employeeId);

            await ValidateForeignKeyAsync<Common.Models.Program>(programId);

            var ep = await _context.FindAsync<EmployeeProgram>(employeeId, programId);
            Validate(
                condition: !(ep is EmployeeProgram),
                message: $"{typeof(T).Name} '{employeeId}' is not in Program '{programId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(ep);
            await _context.SaveChangesAsync();

            var program = await _context.FindAsync<Common.Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramReadDto>(program);
        }

    }
}
