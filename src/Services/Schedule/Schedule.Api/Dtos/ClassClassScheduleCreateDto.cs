using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Dtos
{
    public class ClassClassScheduleCreateDto
    {
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public string RoomId { get; set; }

        [Required]
        public string RoomBuildingId { get; set; }

        public DateTime EndTime { get; set; }
    }
}
