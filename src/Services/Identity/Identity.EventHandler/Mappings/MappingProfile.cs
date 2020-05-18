using AutoMapper;
using Infrastructure.Common.Events;

namespace Identity.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramCreated, Common.Models.Program>();
            CreateMap<ProgramUpdated, Common.Models.Program>();
        }
    }
}
