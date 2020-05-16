using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Dtos
{
    public class ClassScheduleReadDto
    {
        public DateTime StartTime { get; set; }

        public ClassReadDto Class { get; set; }
        public RoomReadDto Room { get; set; }

        public DateTime EndTime { get; set; }
    }
}
