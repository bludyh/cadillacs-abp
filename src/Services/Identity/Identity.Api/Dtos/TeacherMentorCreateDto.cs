using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Dtos
{
    public class TeacherMentorCreateDto
    {

        [Required]
        public int? StudentId { get; set; }

        [Required]
        public string MentorType { get; set; }

    }
}
