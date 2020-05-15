using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Dtos
{
    public class AnnouncementReadDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
    }
}
