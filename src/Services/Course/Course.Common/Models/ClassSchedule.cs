using System;

namespace Course.Common.Models
{
    public class ClassSchedule
    {
        public DateTime StartTime { get; set; }
        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }

        public DateTime EndTime { get; set; }

        public string RoomId { get; set; }
        public string BuildingId { get; set; }
    }
}
