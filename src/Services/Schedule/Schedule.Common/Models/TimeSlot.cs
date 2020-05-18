using System;
using System.Collections.Generic;

namespace Schedule.Common.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
