using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Dtos
{
    public class ProgramUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [Required]
        public int? TotalCredit { get; set; }
    }
}
