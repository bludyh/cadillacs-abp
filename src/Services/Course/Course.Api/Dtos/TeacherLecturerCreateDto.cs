using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class TeacherLecturerCreateDto
    {
        [Required]
        public string ClassId { get; set; }
        [Required]
        public int? ClassSemester { get; set; }
        [Required]
        public int? ClassYear { get; set; }
        [Required]
        public string ClassCourseId { get; set; }
    }
}
