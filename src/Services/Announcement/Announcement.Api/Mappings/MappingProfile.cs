using Announcement.Api.Dtos;
using Announcement.Common.Models;
using AutoMapper;

namespace Announcement.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.Models.Announcement, AnnouncementReadDto>();
            CreateMap<AnnouncementCreateUpdateDto, Common.Models.Announcement>(MemberList.Source);

            CreateMap<ClassAnnouncement, ClassAnnouncementReadDto>();

            CreateMap<Class, ClassReadDto>();
            CreateMap<Employee, EmployeeReadDto>();
        }
    }
}
