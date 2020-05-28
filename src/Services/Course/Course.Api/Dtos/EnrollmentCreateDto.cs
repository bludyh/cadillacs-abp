using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class EnrollmentCreateDto
    {
        [Required]
        public int? StudentId { get; set; }

        public int? GroupId { get; set; }
    }
}
