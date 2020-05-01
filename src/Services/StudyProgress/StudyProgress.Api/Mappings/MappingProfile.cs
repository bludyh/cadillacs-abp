﻿using AutoMapper;
using StudyProgress.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProgress.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Program, ProgramReadDto>();
            CreateMap<ProgramUpdateDto, Models.Program>(MemberList.Source);
            CreateMap<ProgramCreateDto, Models.Program>(MemberList.Source);


        }
    }
}
