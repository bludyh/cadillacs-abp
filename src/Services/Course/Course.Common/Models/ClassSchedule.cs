﻿using System;

namespace Course.Common.Models
{
    public class ClassSchedule
    {
        public int TimeSlotId { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public string RoomId { get; set; }
        public string RoomBuildingId { get; set; }

        public string ClassId { get; set; }
        public int ClassSemester { get; set; }
        public int ClassYear { get; set; }
        public string ClassCourseId { get; set; }
        public Class Class { get; set; }
    }
}
