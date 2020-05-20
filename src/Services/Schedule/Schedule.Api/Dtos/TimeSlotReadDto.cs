using System;

namespace Schedule.Api.Dtos
{
    public class TimeSlotReadDto
    {
        public int Id { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
