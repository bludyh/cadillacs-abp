using System;
using System.ComponentModel.DataAnnotations;

namespace Schedule.Api.Dtos
{
    public class ClassClassScheduleCreateDto
    {
        [Required]
        public int? TimeSlotId { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public string RoomBuildingId { get; set; }
    }
}
