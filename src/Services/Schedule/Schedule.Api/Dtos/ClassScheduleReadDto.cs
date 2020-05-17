﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Dtos
{
    public class ClassScheduleReadDto
    {
        public TimeSlotReadDto TimeSlot { get; set; }
        public DateTime Date { get; set; }
        public RoomReadDto Room { get; set; }

        public ClassReadDto Class { get; set; }
    }
}
