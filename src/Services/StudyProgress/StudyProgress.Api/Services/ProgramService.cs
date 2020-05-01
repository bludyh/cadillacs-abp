using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using StudyProgress.Api.Data;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Services
{
    public interface IProgramService
    {
        public Task<List<ProgramReadDto>> GetAllAsync();
        public Task<ProgramReadDto> GetAsync(int programId);
        public Task UpdateAsync(int programId, ProgramUpdateDto dto);
        public Task<ProgramReadDto> CreateAsync(ProgramCreateDto dto);
        public Task<ProgramReadDto> DeleteAsync(int programId);

        //courses
    }

    public class ProgramService : ServiceBase, IProgramService
    {
        private readonly IMapper _mapper;

        public ProgramService(StudyProgressContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ProgramReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<ProgramReadDto>(_context.Set<Models.Program>()).ToListAsyncFallback();
        }

        public async Task<ProgramReadDto> GetAsync(int programId)
        {
            var program = await ValidateExistenceAsync<Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task UpdateAsync(int programId, ProgramUpdateDto dto)
        {
            var program = await ValidateExistenceAsync<Models.Program>(programId);

            _mapper.Map(dto, program);

            _context.Update(program);
            await _context.SaveChangesAsync();
        }

        public async Task<ProgramReadDto> CreateAsync(ProgramCreateDto dto)
        {
            // Unsure if correct
            await ValidateExistenceAsync<Models.Program>(dto.Name);

            await ValidateForeignKeyAsync<School>(dto.SchoolId);

            var program = _mapper.Map<Models.Program>(dto);

            return _mapper.Map<ProgramReadDto>(program);
        }

        public async Task<ProgramReadDto> DeleteAsync(int programId)
        {
            var program = await ValidateExistenceAsync<Models.Program>(programId);

            await _context.Entry(program)
                .Reference(p => p.School)
                .LoadAsync();

            _context.Remove(program);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProgramReadDto>(program);
        }
    }
}
