using Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos
{
    public class StudentMentorCreateDto
    {

        [Required]
        public int? TeacherId { get; set; }

        [Required]
        public string MentorType { get; set; }

    }
}
