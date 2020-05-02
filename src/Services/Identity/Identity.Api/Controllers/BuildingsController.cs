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
using Bogus.DataSets;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            return await _buildingService.GetAllAsync();
        }

        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(string id)
        {
            return await _buildingService.GetAsync(id);
        }

        // POST: api/Buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BuildingDto>> PostBuilding([FromBody, Required] string buildingId)
        {
            var building = await _buildingService.CreateAsync(buildingId);

            return CreatedAtAction(nameof(GetBuilding), new { id = building.Id }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BuildingDto>> DeleteBuilding(string id)
        {
            return await _buildingService.DeleteAsync(id);
        }

        // Rooms

        [HttpGet("{buildingId}/rooms")]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms(string buildingId)
        {
            return await _buildingService.GetRoomsAsync(buildingId);
        }

        [HttpGet("{buildingId}/rooms/{roomId}")]
        public async Task<ActionResult<RoomDto>> GetRoom(string buildingId, string roomId)
        {
            return await _buildingService.GetRoomAsync(buildingId, roomId);
        }

        [HttpPost("{buildingId}/rooms")]
        public async Task<ActionResult<RoomDto>> AddRoom(string buildingId, [FromBody, Required] string roomId)
        {
            var room = await _buildingService.AddRoomAsync(buildingId, roomId);

            return CreatedAtAction(nameof(GetRoom), new { buildingId, roomId }, room);
        }

        [HttpDelete("{buildingId}/rooms/{roomId}")]
        public async Task<ActionResult<RoomDto>> RemoveRoom(string buildingId, string roomId)
        {
            return await _buildingService.RemoveRoomAsync(buildingId, roomId);
        }

        // Schools

        [HttpGet("{buildingId}/schools")]
        public async Task<ActionResult<IEnumerable<SchoolCreateReadDto>>> GetSchools(string buildingId)
        {
            return await _buildingService.GetSchoolsAsync(buildingId);
        }

        [HttpPost("{buildingId}/schools")]
        public async Task<ActionResult<SchoolCreateReadDto>> AddSchool(string buildingId, [FromBody, Required] string schoolId)
        {
            var school = await _buildingService.AddSchoolAsync(buildingId, schoolId);

            return CreatedAtAction(nameof(GetSchools), new { buildingId }, school);
        }

        [HttpDelete("{buildingId}/schools/{schoolId}")]
        public async Task<ActionResult<SchoolCreateReadDto>> RemoveSchool(string buildingId, string schoolId)
        {
            return await _buildingService.RemoveSchoolAsync(buildingId, schoolId);
        }

    }
}
