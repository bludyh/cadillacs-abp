using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class GroupSubmissionCreateUpdateDto
    {
        [Required]
        public int? GroupId { get; set; }
        [Required]
        public int? AssignmentId { get; set; }
        [Required]
        public int? AttachmentId { get; set; }
        public double? Grade { get; set; }
    }
}
