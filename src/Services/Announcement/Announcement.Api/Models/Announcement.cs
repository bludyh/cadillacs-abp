using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Models
{
    public class Announcement
    {
        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateTime { get; set; }

        public Employee Employee { get; set; }

        public List<ClassAnnouncement> ClassAnnouncements { get; set; }
    }
}
