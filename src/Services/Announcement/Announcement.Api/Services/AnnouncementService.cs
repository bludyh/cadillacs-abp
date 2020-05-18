using Announcement.Api.Data;
using Announcement.Api.Dtos;
using Announcement.Api.Models;
using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Services
{
    public interface IAnnouncementService
    {
        public Task<List<AnnouncementReadDto>> GetAllAsync();
        public Task<AnnouncementReadDto> GetAsync(int announcementId);
        public Task UpdateAsync(int announcementId, AnnouncementCreateUpdateDto dto);
        public Task<AnnouncementReadDto> CreateAsync(AnnouncementCreateUpdateDto dto);
        public Task<AnnouncementReadDto> DeleteAsync(int announcementId);
    }
    public class AnnouncementService : ServiceBase, IAnnouncementService
    {
        private readonly IMapper _mapper;

        public AnnouncementService(AnnouncementContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region Announcements
        public async Task<List<AnnouncementReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<AnnouncementReadDto>(_context.Set<Models.Announcement>()).ToListAsyncFallback();
        }

        public async Task<AnnouncementReadDto> GetAsync(int announcementId)
        {
            var announcement = await ValidateExistenceAsync<Models.Announcement>(announcementId);

            await _context.Entry(announcement)
                .Reference(a => a.Employee)
                .LoadAsync();

            return _mapper.Map<AnnouncementReadDto>(announcement);
        }

        public async Task UpdateAsync(int announcementId, AnnouncementCreateUpdateDto dto)
        {
            var announcement = await ValidateExistenceAsync<Models.Announcement>(announcementId);

            _mapper.Map(dto, announcement);

            _context.Update(announcement);
            await _context.SaveChangesAsync();
        }

        public async Task<AnnouncementReadDto> CreateAsync(AnnouncementCreateUpdateDto dto)
        {
            await ValidateForeignKeyAsync<Employee>(dto.EmployeeId);

            var announcement = _mapper.Map<Models.Announcement>(dto);

            // Add current DateTime at announcement creation, formatted to Sortable ("s") format to remove milliseconds.
            announcement.DateTime = Convert.ToDateTime(DateTime.Now.ToString("s"));

            await _context.AddAsync(announcement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AnnouncementReadDto>(announcement);
        }

        public async Task<AnnouncementReadDto> DeleteAsync(int announcementId)
        {
            var announcement = await ValidateExistenceAsync<Models.Announcement>(announcementId);

            await _context.Entry(announcement)
                .Reference(a => a.Employee)
                .LoadAsync();

            _context.Remove(announcement);
            await _context.SaveChangesAsync();

            return _mapper.Map<AnnouncementReadDto>(announcement);
        }
        #endregion
    }
}
