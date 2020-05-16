using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class ClassSchedule
    {
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
    
        public DateTime EndTime { get; set; }

        public string ClassId { get; set; }

        public int ClassSemester { get; set; }

        public int ClassYear { get; set; }

        public string ClassCourseId { get; set; }

        public string RoomId { get; set; }

        public string RoomBuildingId { get; set; }

        public Class Class { get; set; }

        public Room Room { get; set; }

    }
}
