using System.ComponentModel.DataAnnotations;

namespace Announcement.Api.Dtos
{
    public class AnnouncementCreateUpdateDto
    {
        [Required]
        public int? EmployeeId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }
    }
}
