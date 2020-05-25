using AutoMapper;
using Course.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.Models.Course, CourseReadDto>();
            CreateMap<CourseUpdateDto, Common.Models.Course>(MemberList.Source);
            CreateMap<CourseCreateDto, Common.Models.Course>(MemberList.Source);
        }
    }
}
