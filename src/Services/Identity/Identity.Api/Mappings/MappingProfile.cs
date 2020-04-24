using AutoMapper;
using Identity.Api.Dtos;
using Identity.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Mappings {
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<Student, StudentReadDto>()
                .ForMember(dest => dest.Pcn, opt => opt.MapFrom(src => src.UserName));
            CreateMap<StudentCreateDto, Student>(MemberList.Source);
            CreateMap<StudentUpdateDto, Student>(MemberList.Source);
            CreateMap<Models.Program, ProgramDto>();
        }

    }
}
