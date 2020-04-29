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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IdentityContext _context;
        private readonly IMapper _mapper;

        public SchoolsController(IdentityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolCreateReadDto>>> GetSchools()
        {
            // Use ToListAsyncFallback extension method from Infrastructure.Common to support IQueryable that does not implement IAsyncEnumerable
            // https://github.com/dotnet/efcore/issues/9179#issuecomment-479863685
            return await _mapper.ProjectTo<SchoolCreateReadDto>(_context.Schools).ToListAsyncFallback();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolCreateReadDto>> GetSchool(string id)
        {
            if (!(await _context.FindAsync<School>(id) is School school))
                return NotFound();

            return _mapper.Map<SchoolCreateReadDto>(school);
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(string id, SchoolUpdateDto dto)
        {
            if (!(await _context.FindAsync<School>(id) is School school))
                return NotFound();

            _mapper.Map(dto, school);

            _context.Update(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Schools
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(SchoolCreateReadDto dto)
        {
            if (await _context.FindAsync<School>(dto.Id) != null)
                return Conflict();

            var school = _mapper.Map<School>(dto);

            await _context.Schools.AddAsync(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<School>> DeleteSchool(string id)
        {
            if (!(await _context.FindAsync<School>(id) is School school))
                return NotFound();

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return school;
        }

        // Buildings

        [HttpGet("{id}/buildings")]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings(string id)
        {
            if (!(await _context.FindAsync<School>(id) is School school))
                return NotFound();

            await _context.Entry(school)
                .Collection(s => s.SchoolBuildings)
                .Query()
                .Include(sb => sb.Building)
                .LoadAsync();

            return await _mapper.ProjectTo<BuildingDto>(school.SchoolBuildings.Select(sb => sb.Building).AsQueryable()).ToListAsyncFallback();
        }

        [HttpPost("{id}/buildings")]
        public async Task<ActionResult<SchoolBuilding>> AddBuilding(string id, [FromBody, Required] string buildingId)
        {
            if (!(await _context.FindAsync<School>(id) is School school))
                return NotFound();

            if (!(await _context.FindAsync<Building>(buildingId) is Building building))
                return UnprocessableEntity();

            if (await _context.FindAsync<SchoolBuilding>(id, buildingId) != null)
                return Conflict();

            var sb = new SchoolBuilding { School = school, Building = building };

            await _context.AddAsync(sb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuildings", new { id = sb.SchoolId }, sb);
        }

        [HttpDelete("{schoolId}/buildings/{buildingId}")]
        public async Task<ActionResult<SchoolBuilding>> RemoveBuilding(string schoolId, string buildingId)
        {
            if (!(await _context.FindAsync<SchoolBuilding>(schoolId, buildingId) is SchoolBuilding sb))
                return NotFound();

            _context.Remove(sb);
            await _context.SaveChangesAsync();

            return sb;
        }

    }
}
