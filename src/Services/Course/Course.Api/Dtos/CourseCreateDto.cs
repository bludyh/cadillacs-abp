using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class CourseCreateDto
    {
        [Required]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int? Credit { get; set; }
    }
}
