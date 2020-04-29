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

        public int ClassId { get; set; }

        public int Semester { get; set; }

        public int Year { get; set; }

        public int CourseId { get; set; }

        public string RoomId { get; set; }

        public string Building { get; set; }

        public Class Class { get; set; }

        public Room Room { get; set; }

    }
}
