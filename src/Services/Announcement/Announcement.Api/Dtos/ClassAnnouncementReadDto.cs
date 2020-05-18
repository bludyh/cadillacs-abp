using Announcement.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Dtos
{
    public class ClassAnnouncementReadDto
    {
        public AnnouncementReadDto Announcement { get; set; }
        public ClassReadDto Class { get; set; }
    }
}
