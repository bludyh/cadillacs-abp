using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Models
{
    public class Class
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }

        public List<ClassAnnouncement> ClassAnnouncements{ get; set; }
    }
}
