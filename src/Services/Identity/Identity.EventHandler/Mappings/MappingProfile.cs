using AutoMapper;
using Identity.EventHandler.Models;
using Infrastructure.Common.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProgramCreated, Models.Program>();
            CreateMap<ProgramUpdated, Models.Program>();
        }
    }
}
