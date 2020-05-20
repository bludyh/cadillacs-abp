using AutoMapper;
using Identity.Api.Dtos;
using Identity.Common.Data;
using Identity.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Events;
using Infrastructure.Common.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pitstop.Infrastructure.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface IBuildingService
    {
        public Task<List<BuildingReadDto>> GetAllAsync();
        public Task<BuildingReadDto> GetAsync(string buildingId);
        public Task<BuildingReadDto> CreateAsync(string buildingId);
        public Task<BuildingReadDto> DeleteAsync(string buildingId);
        public Task<List<RoomReadDto>> GetRoomsAsync(string buildingId);
        public Task<RoomReadDto> GetRoomAsync(string buildingId, string roomId);
        public Task<RoomReadDto> AddRoomAsync(string buildingId, string roomId);
        public Task<RoomReadDto> RemoveRoomAsync(string buildingId, string roomId);
        public Task<List<SchoolCreateReadDto>> GetSchoolsAsync(string buildingId);
        public Task<SchoolCreateReadDto> AddSchoolAsync(string buildingId, string schoolId);
        public Task<SchoolCreateReadDto> RemoveSchoolAsync(string buildingId, string schoolId);
    }

    public class BuildingService : ServiceBase, IBuildingService
    {
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;

        public BuildingService(IdentityContext context, IMapper mapper, IMessagePublisher messagePublisher) : base(context)
        {
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        public async Task<RoomReadDto> AddRoomAsync(string buildingId, string roomId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);
            await ValidateDuplicationAsync<Room>(roomId, buildingId);

            var room = new Room { Id = roomId, Building = building };

            await _context.AddAsync(room);
            await _context.SaveChangesAsync();

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            // Publish event
            var e = _mapper.Map<RoomCreated>(room);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<RoomReadDto>(room);
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

        public async Task<BuildingReadDto> CreateAsync(string buildingId)
        {
            await ValidateDuplicationAsync<Building>(buildingId);

            var building = new Building { Id = buildingId };

            await _context.AddAsync(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingReadDto>(building);
        }

        public async Task<BuildingReadDto> DeleteAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            _context.Remove(building);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingReadDto>(building);
        }

        public async Task<List<BuildingReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<BuildingReadDto>(_context.Set<Building>()).ToListAsyncFallback();
        }

        public async Task<BuildingReadDto> GetAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            return _mapper.Map<BuildingReadDto>(building);
        }

        public async Task<RoomReadDto> GetRoomAsync(string buildingId, string roomId)
        {
            var room = await ValidateExistenceAsync<Room>(roomId, buildingId);

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            return _mapper.Map<RoomReadDto>(room);
        }

        public async Task<List<RoomReadDto>> GetRoomsAsync(string buildingId)
        {
            var building = await ValidateExistenceAsync<Building>(buildingId);

            await _context.Entry(building)
                .Collection(b => b.Rooms)
                .LoadAsync();

            return await _mapper.ProjectTo<RoomReadDto>(building.Rooms.AsQueryable()).ToListAsyncFallback();
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

        public async Task<RoomReadDto> RemoveRoomAsync(string buildingId, string roomId)
        {
            var room = await ValidateExistenceAsync<Room>(roomId, buildingId);

            await _context.Entry(room)
                .Reference(r => r.Building)
                .LoadAsync();

            _context.Remove(room);
            await _context.SaveChangesAsync();

            // Publish event
            var e = _mapper.Map<RoomDeleted>(room);
            await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

            return _mapper.Map<RoomReadDto>(room);
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
