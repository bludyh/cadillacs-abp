﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Dtos
{
    public class LecturerCreateDto
    {
        [Required]
        public int? TeacherId { get; set; }
    }
}
