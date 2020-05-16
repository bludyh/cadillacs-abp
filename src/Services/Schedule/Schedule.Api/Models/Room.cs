using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class Room
    {
        public string Id { get; set; }
        public string BuildingId { get; set; }

        public List<ClassSchedule> ClassSchedules { get; set; }
    }
}
