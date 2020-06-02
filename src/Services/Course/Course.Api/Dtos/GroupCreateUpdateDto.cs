using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class GroupCreateUpdateDto
    {
        public string Name { get; set; }
        [Required]
        public int? MaxSize { get; set; }
    }
}
