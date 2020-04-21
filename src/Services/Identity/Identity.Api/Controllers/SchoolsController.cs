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

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly IdentityContext _context;

        public SchoolsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            return await _context.Schools
                .Select(s => new SchoolDto {
                    Id = s.Id,
                    Name = s.Name,
                    StreetName = s.StreetName,
                    HouseNumber = s.HouseNumber,
                    PostalCode = s.PostalCode,
                    City = s.City,
                    Country = s.Country
                })
                .ToListAsync();
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            var result = new SchoolDto { 
                Id = school.Id,
                Name = school.Name,
                StreetName = school.StreetName,
                HouseNumber = school.HouseNumber,
                PostalCode = school.PostalCode,
                City = school.City,
                Country = school.Country
            };

            return result;
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(string id, SchoolDto schoolDto)
        {
            if (id != schoolDto.Id)
            {
                return BadRequest();
            }

            var school = await _context.Schools.FindAsync(id);

            if (school == null) {
                return NotFound();
            }

            school.Name = schoolDto.Name;
            school.StreetName = schoolDto.StreetName;
            school.HouseNumber = schoolDto.HouseNumber;
            school.PostalCode = schoolDto.PostalCode;
            school.City = schoolDto.City;
            school.Country = schoolDto.Country;
            _context.Entry(school).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Schools
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(SchoolDto schoolDto)
        {
            var school = await _context.Schools.FindAsync(schoolDto.Id);
            if (school != null)
                return Conflict();

            school = new School {
                Id = schoolDto.Id,
                Name = schoolDto.Name,
                StreetName = schoolDto.StreetName,
                HouseNumber = schoolDto.HouseNumber,
                PostalCode = schoolDto.PostalCode,
                City = schoolDto.City,
                Country = schoolDto.Country
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<School>> DeleteSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return school;
        }

    }
}
