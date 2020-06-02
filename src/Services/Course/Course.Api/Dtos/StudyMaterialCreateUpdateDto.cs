using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class StudyMaterialCreateUpdateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Week { get; set; }
    }
}
