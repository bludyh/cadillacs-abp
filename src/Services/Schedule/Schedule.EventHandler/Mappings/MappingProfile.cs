using AutoMapper;
using Infrastructure.Common.Events;
using Schedule.Common.Models;

namespace Schedule.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<ClassCreated, Class>();
            CreateMap<ClassUpdated, Class>();
            CreateMap<RoomCreated, Room>();
        }

    }
}
