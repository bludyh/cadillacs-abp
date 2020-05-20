using System;

namespace Announcement.Api.Dtos
{
    public class AnnouncementReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }

        public EmployeeReadDto Employee { get; set; }
    }
}
