﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Dtos
{
    public class AnnouncementCreateUpdateDto
    {
        [Required]
        public int? EmployeeId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        // TODO: Get current DateTime in code at creation
    }
}
