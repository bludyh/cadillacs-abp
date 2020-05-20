using Announcement.Common.Models;
using AutoMapper;
using Infrastructure.Common.Events;

namespace Announcement.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<EmployeeCreated, Employee>();
            CreateMap<EmployeeUpdated, Employee>();
            CreateMap<ClassCreated, Class>();
            CreateMap<ClassUpdated, Class>();
        }

    }
}
