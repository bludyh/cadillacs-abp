using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Dtos
{
    public class ClassScheduleReadDto
    {
        public string Day { get; set; }

        //Not sure cuz room has BuildingId
        public string Building { get; set; }

        public ClassReadDto Class { get; set; }

        public RoomReadDto Room { get; set; }

    }
}
