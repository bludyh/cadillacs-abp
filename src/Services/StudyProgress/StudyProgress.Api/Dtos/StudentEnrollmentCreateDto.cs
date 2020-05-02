using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Dtos
{
    public class StudentEnrollmentCreateDto
    {
        [Required]
        public string ClassId { get; set; }
        
        [Required]
        public int? ClassSemester { get; set; }
        
        [Required]
        public int? ClassYear { get; set; }
        
        [Required]
        public int? ClassCourseId { get; set; }

    }
}
