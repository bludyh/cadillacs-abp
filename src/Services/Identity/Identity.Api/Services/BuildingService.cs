using AutoMapper;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface IBuildingService
    {
        public Task<List<BuildingDto>> GetAllAsync();
        public Task<BuildingDto> GetAsync(string buildingId);
        public Task<BuildingDto> CreateAsync(string buildingId);
        public Task<BuildingDto> DeleteAsync(string buildingId);
        public Task<List<RoomDto>> GetRoomsAsync(string buildingId);
        public Task<RoomDto> GetRoomAsync(string buildingId, string roomId);
        public Task<RoomDto> AddRoomAsync(string buildingId, string roomId);
        public Task<RoomDto> RemoveRoomAsync(string buildingId, string roomId);
        public Task<List<SchoolCreateReadDto>> GetSchoolsAsync(string buildingId);
        public Task<SchoolCreateReadDto> AddSchoolAsync(string buildingId, string schoolId);
        public Task<SchoolCreateReadDto> RemoveSchoolAsync(string buildingId, string schoolId);
    }

    public class BuildingService : ServiceBase, IBuildingService
    {
        private readonly IMapper _mapper;

        public BuildingService(IdentityContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<RoomDto> AddRoomAsync(string buildingId, string roomId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);
            await ValidateDuplicationAsync<Room>(roomId, buildingId);

            var room = new Room { Id = roomId, Building = building };

            await _context.AddAsync(room);
            await _context.SaveChangesAsync();

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<SchoolCreateReadDto> AddSchoolAsync(string buildingId, string schoolId)
        {
            await ValidateExistenceAsync<Building>(buildingId);
            var school = await ValidateForeignKeyAsync<School>(schoolId);
            await ValidateDuplicationAsync<SchoolBuilding>(schoolId, buildingId);

            var sb = new SchoolBuilding { SchoolId = schoolId, BuildingId = buildingId };

            await _context.AddAsync(sb);
            await _context.SaveChangesAsync();

            return _mapper.Map<SchoolCreateReadDto>(school);
        }

        public async Task<BuildingDto> CreateAsync(string buildingId)
        {
            await ValidateDuplicationAsync<Building>(buildingId);

            var building = new Building { Id = buildingId };

            await _context.AddAsync(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDto>(building);
        }

        public async Task<BuildingDto> DeleteAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            _context.Remove(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDto>(building);
        }

        public async Task<List<BuildingDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<BuildingDto>(_context.Set<Building>()).ToListAsyncFallback();
        }

        public async Task<BuildingDto> GetAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            return _mapper.Map<BuildingDto>(building);
        }

        public async Task<RoomDto> GetRoomAsync(string buildingId, string roomId)
        {
            var room = await ValidateExistenceAsync<Room>(roomId, buildingId);

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<List<RoomDto>> GetRoomsAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            await _context.Entry(building)
                .Collection(b => b.Rooms)
                .LoadAsync();

            return await _mapper.ProjectTo<RoomDto>(building.Rooms.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<List<SchoolCreateReadDto>> GetSchoolsAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            await _context.Entry(building)
                .Collection(b => b.SchoolBuildings)
                .Query()
                .Include(sb => sb.School)
                .LoadAsync();

            return await _mapper.ProjectTo<SchoolCreateReadDto>(building.SchoolBuildings.Select(sb => sb.School).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<RoomDto> RemoveRoomAsync(string buildingId, string roomId)
        {
            var room = await ValidateExistenceAsync<Room>(roomId, buildingId);

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            _context.Remove(room);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task<SchoolCreateReadDto> RemoveSchoolAsync(string buildingId, string schoolId)
        {
            await ValidateExistenceAsync<Building>(buildingId);
            var school = await ValidateForeignKeyAsync<School>(schoolId);

            var sb = await _context.FindAsync<SchoolBuilding>(schoolId, buildingId);
            Validate(
                condition: !(sb is SchoolBuilding),
                message: $"School '{schoolId}' is not in Building '{buildingId}'.",
                status: StatusCodes.Status404NotFound);

            _context.Remove(sb);
            await _context.SaveChangesAsync();

            return _mapper.Map<SchoolCreateReadDto>(school);
        }
    }
}
