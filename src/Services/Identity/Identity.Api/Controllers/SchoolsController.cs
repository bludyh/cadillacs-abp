using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Identity.Api.Data;
using Identity.Api.Models;
using Identity.Api.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using Identity.Api.Services;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolCreateReadDto>>> GetSchools()
        {
            return await _schoolService.GetAllAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolCreateReadDto>> GetSchool([FromRoute] string id)
        {
            return await _schoolService.GetAsync(id);
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool([FromRoute] string id, [FromBody] SchoolUpdateDto dto)
        {
            await _schoolService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Schools
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SchoolCreateReadDto>> PostSchool([FromBody] SchoolCreateReadDto dto)
        {
            var school = await _schoolService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SchoolCreateReadDto>> DeleteSchool([FromRoute] string id)
        {
            return await _schoolService.DeleteAsync(id);
        }

        // Buildings

        [HttpGet("{id}/buildings")]
        public async Task<ActionResult<IEnumerable<BuildingReadDto>>> GetBuildings([FromRoute] string id)
        {
            return await _schoolService.GetBuildingsAsync(id);
        }

        [HttpPost("{id}/buildings")]
        public async Task<ActionResult<BuildingReadDto>> AddBuilding([FromRoute] string id, [FromBody, Required] string buildingId)
        {
            var building = await _schoolService.AddBuildingAsync(id, buildingId);

            return CreatedAtAction("GetBuildings", new { id }, building);
        }

        [HttpDelete("{schoolId}/buildings/{buildingId}")]
        public async Task<ActionResult<BuildingReadDto>> RemoveBuilding([FromRoute] string schoolId, [FromRoute] string buildingId)
        {
            return await _schoolService.RemoveBuildingAsync(schoolId, buildingId);
        }

    }
}
