using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
