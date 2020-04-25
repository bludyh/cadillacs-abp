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
            return await _mapper.ProjectTo<SchoolCreateReadDto>(_context.Schools).ToListAsync();
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
            if (dto.Name == null)
                return BadRequest();

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
            if (dto.Id == null
                || dto.Name == null)
                return BadRequest();

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

    }
}
