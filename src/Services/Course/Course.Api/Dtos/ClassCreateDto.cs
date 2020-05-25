using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class ClassCreateDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int? Semester { get; set; }
        [Required]
        public int? Year { get; set; }
    }
}
