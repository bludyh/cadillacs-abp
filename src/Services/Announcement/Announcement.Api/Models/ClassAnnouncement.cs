using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Models
{
    public class ClassAnnouncement
    {
        public string AnnouncementId { get; set; }

        public int ClassId { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public string CourseId { get; set; }

        public Announcement Announcement { get; set; }

        public Class Class { get; set; }
    }
}
