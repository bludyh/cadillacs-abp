﻿namespace Announcement.Common.Models
{
    public class ClassAnnouncement
    {
        public int AnnouncementId { get; set; }
        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }

        public Announcement Announcement { get; set; }
        public Class Class { get; set; }
    }
}
