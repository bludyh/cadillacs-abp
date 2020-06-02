using AutoMapper;
using Course.Api.Dtos;
using Course.Common.Data;
using Course.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Services
{
    public interface IAttachmentService
    {
        public Task<List<AttachmentReadDto>> GetAllAsync();
        public Task<AttachmentReadDto> GetAsync(int id);
        public Task UpdateAsync(int id, AttachmentCreateUpdateDto dto);
        public Task<AttachmentReadDto> CreateAsync(AttachmentCreateUpdateDto dto);
        public Task<AttachmentReadDto> DeleteAsync(int id);
    }

    public class AttachmentService : ServiceBase, IAttachmentService
    {
        private readonly IMapper _mapper;

        public AttachmentService(CourseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<AttachmentReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<AttachmentReadDto>(_context.Set<Attachment>()).ToListAsyncFallback();
        }

        public async Task<AttachmentReadDto> GetAsync(int id)
        {
            var attachment = await ValidateExistenceAsync<Attachment>(id);

            return _mapper.Map<AttachmentReadDto>(attachment);
        }

        public async Task UpdateAsync(int id, AttachmentCreateUpdateDto dto)
        {
            var attachment = await ValidateExistenceAsync<Attachment>(id);

            _mapper.Map(dto, id);
            _context.Update(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task<AttachmentReadDto> CreateAsync(AttachmentCreateUpdateDto dto)
        {
            var attachment = _mapper.Map<Attachment>(dto);

            await _context.AddAsync(attachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttachmentReadDto>(attachment);
        }

        public async Task<AttachmentReadDto> DeleteAsync(int id)
        {
            var attachment = await ValidateExistenceAsync<Attachment>(id);

            _context.Remove(attachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AttachmentReadDto>(attachment);
        }
    }
}
