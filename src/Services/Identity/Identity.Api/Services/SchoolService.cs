using AutoMapper;
using Identity.Api.Data;
using Identity.Api.Dtos;
using Identity.Api.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Services
{

    public interface ISchoolService
    {
        public Task<List<SchoolCreateReadDto>> GetAllAsync();
        public Task<SchoolCreateReadDto> GetAsync(string schoolId);
        public Task UpdateAsync(string schoolId, SchoolUpdateDto dto);
        public Task<SchoolCreateReadDto> CreateAsync(SchoolCreateReadDto dto);
        public Task<SchoolCreateReadDto> DeleteAsync(string schoolId);
        public Task<List<BuildingDto>> GetBuildingsAsync(string schoolId);
        public Task<BuildingDto> AddBuildingAsync(string schoolId, string buildingId);
        public Task<BuildingDto> RemoveBuildingAsync(string schoolId, string buildingId);
    }

    public class SchoolService : ServiceBase, ISchoolService
    {
        private readonly IMapper _mapper;

        public SchoolService(IdentityContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BuildingDto> AddBuildingAsync(string schoolId, string buildingId)
        {
            var school = await ValidateExistenceAsync<School>(schoolId);
            var building = await ValidateForeignKeyAsync<Building>(buildingId);
            await ValidateDuplicationAsync<SchoolBuilding>(schoolId, buildingId);

            var sb = new SchoolBuilding { School = school, Building = building };

            await _context.AddAsync(sb);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDto>(building);
        }

        public async Task<SchoolCreateReadDto> CreateAsync(SchoolCreateReadDto dto)
        {
            await ValidateDuplicationAsync<School>(dto.Id);

            var school = _mapper.Map<School>(dto);

            await _context.AddAsync(school);
            await _context.SaveChangesAsync();

            return _mapper.Map<SchoolCreateReadDto>(school);
        }

        public async Task<SchoolCreateReadDto> DeleteAsync(string schoolId)
        {
            var school = await ValidateExistenceAsync<School>(schoolId);

            _context.Remove(school);
            await _context.SaveChangesAsync();

            return _mapper.Map<SchoolCreateReadDto>(school);
        }

        public async Task<List<SchoolCreateReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<SchoolCreateReadDto>(_context.Set<School>()).ToListAsyncFallback();
        }

        public async Task<SchoolCreateReadDto> GetAsync(string schoolId)
        {
            var school = await ValidateExistenceAsync<School>(schoolId);

            return _mapper.Map<SchoolCreateReadDto>(school);
        }

        public async Task<List<BuildingDto>> GetBuildingsAsync(string schoolId)
        {
            var school = await ValidateExistenceAsync<School>(schoolId);

            await _context.Entry(school)
                .Collection(s => s.SchoolBuildings)
                .Query()
                .Include(sb => sb.Building)
                .LoadAsync();

            return await _mapper.ProjectTo<BuildingDto>(school.SchoolBuildings.Select(sb => sb.Building).AsQueryable()).ToListAsyncFallback();
        }

        public async Task<BuildingDto> RemoveBuildingAsync(string schoolId, string buildingId)
        {
            await ValidateExistenceAsync<School>(schoolId);
            var building = await ValidateForeignKeyAsync<Building>(buildingId);
            var sb = await ValidateExistenceAsync<SchoolBuilding>(schoolId, buildingId);

            _context.Remove(sb);
            await _context.SaveChangesAsync();

            return _mapper.Map<BuildingDto>(building);
        }

        public async Task UpdateAsync(string schoolId, SchoolUpdateDto dto)
        {
            var school = await ValidateExistenceAsync<School>(schoolId);

            _mapper.Map(dto, school);

            _context.Update(school);
            await _context.SaveChangesAsync();
        }
    }
}
