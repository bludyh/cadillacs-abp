using Announcement.Api.Dtos;
using Announcement.Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Announcement, AnnouncementReadDto>();
            CreateMap<AnnouncementCreateUpdateDto, Models.Announcement>(MemberList.Source);

            CreateMap<ClassAnnouncement, ClassAnnouncementReadDto>();
        }
    }
}
