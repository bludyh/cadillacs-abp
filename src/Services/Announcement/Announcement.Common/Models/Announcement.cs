using System;
using System.Collections.Generic;

namespace Announcement.Common.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }

        public Employee Employee { get; set; }
        public List<ClassAnnouncement> ClassAnnouncements { get; set; }
    }
}
