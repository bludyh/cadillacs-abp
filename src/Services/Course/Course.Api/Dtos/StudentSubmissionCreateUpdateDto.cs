using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class StudentSubmissionCreateUpdateDto
    {
        [Required]
        public int? StudentId { get; set; }
        [Required]
        public int? AssignmentId { get; set; }
        [Required]
        public int? AttachmentId { get; set; }
        public double? Grade { get; set; }
    }
}
