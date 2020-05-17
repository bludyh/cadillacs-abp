using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Dtos
{
    public class ClassClassScheduleCreateDeleteDto
    {
        [Required]
        public int? TimeSlotId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public string RoomBuildingId { get; set; }
    }
}
