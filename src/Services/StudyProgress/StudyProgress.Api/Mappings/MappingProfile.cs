using AutoMapper;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Models;
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


            CreateMap<Enrollment, StudentEnrollmentReadDto>();
            CreateMap<StudentEnrollmentCreateDto, Enrollment>(MemberList.Source);
            //CreateMap<EmployeeCreateDto, Student>(MemberList.Source);


            CreateMap<Course, CourseReadDto>();

        }
    }
}
