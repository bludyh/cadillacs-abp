using Course.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class AssignmentCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public AssignmentType Type { get; set; }
        public string Description { get; set; }
        public DateTime DeadlineDateTime { get; set; }
        [Required]
        public int? Weight { get; set; }
    }
}
