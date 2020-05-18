using Announcement.Api.Dtos;
using Announcement.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Announcement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        #region Announcements
        // GET: api/Announcements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementReadDto>>> GetAnnouncements()
        {
            return await _announcementService.GetAllAsync();
        }

        // GET: api/Announcements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementReadDto>> GetAnnouncement(int id)
        {
            return await _announcementService.GetAsync(id);
        }

        // PUT: api/Announcements/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAnnouncement(int id, AnnouncementCreateUpdateDto dto)
        {
            await _announcementService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Announcements
        [HttpPost]
        public async Task<ActionResult<AnnouncementReadDto>> PostAnnouncement(AnnouncementCreateUpdateDto dto)
        {
            var announcement = await _announcementService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAnnouncement), new { id = announcement.Id }, announcement);
        }

        // DELETE: api/Announcements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnnouncementReadDto>> DeleteAnnouncement(int id)
        {
            return await _announcementService.DeleteAsync(id);
        }
        #endregion
    }
}