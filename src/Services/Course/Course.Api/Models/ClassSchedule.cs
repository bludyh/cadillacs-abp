using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class ClassSchedule
    {
        public string Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string RoomId { get; set; }
        public string BuildingId { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public int ClassCourseId { get; set; }
        public Class Class { get; set; }
    }
}
