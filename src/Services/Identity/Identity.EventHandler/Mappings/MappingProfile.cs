using AutoMapper;
using Identity.EventHandler.Events;
using Identity.EventHandler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramCreated, Models.Program>(MemberList.Source);
            CreateMap<ProgramUpdated, Models.Program>(MemberList.Source);
        }
    }
}
