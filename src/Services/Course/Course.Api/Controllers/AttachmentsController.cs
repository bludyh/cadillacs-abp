using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Dtos;
using Course.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        // GET: api/Attachments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttachmentReadDto>>> GetAttachments()
        {
            return await _attachmentService.GetAllAsync();
        }

        // GET: api/Attachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttachmentReadDto>> GetAttachment([FromRoute] int id)
        {
            return await _attachmentService.GetAsync(id);
        }

        // PUT: api/Attachments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachment([FromRoute] int id, [FromBody] AttachmentCreateUpdateDto dto)
        {
            await _attachmentService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Attachments
        [HttpPost]
        public async Task<ActionResult<AttachmentReadDto>> PostAttachment([FromBody] AttachmentCreateUpdateDto dto)
        {
            var attachment = await _attachmentService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetAttachments), new { id = attachment.Id }, attachment);
        }

        // DELETE: api/Attachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AttachmentReadDto>> DeleteAttachment([FromRoute] int id)
        {
            return await _attachmentService.DeleteAsync(id);
        }
    }
}