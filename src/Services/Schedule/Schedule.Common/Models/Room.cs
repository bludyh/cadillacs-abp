using System.Collections.Generic;

namespace Schedule.Common.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
