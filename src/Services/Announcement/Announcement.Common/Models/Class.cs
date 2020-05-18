using System.Collections.Generic;

namespace Announcement.Common.Models
{
    public class Class
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }

        public string CourseName { get; set; }

        public List<ClassAnnouncement> ClassAnnouncements { get; set; }
    }
}
