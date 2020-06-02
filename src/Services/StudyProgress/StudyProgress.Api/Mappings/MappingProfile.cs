using AutoMapper;
using Infrastructure.Common.Events;
using StudyProgress.Api.Dtos;
using StudyProgress.Common.Models;

namespace StudyProgress.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.Models.Program, ProgramReadDto>();
            CreateMap<ProgramUpdateDto, Common.Models.Program>(MemberList.Source);
            CreateMap<ProgramCreateDto, Common.Models.Program>(MemberList.Source);

            CreateMap<Enrollment, StudentEnrollmentReadDto>();

            CreateMap<Class, ClassReadDto>();
            CreateMap<Course, CourseReadDto>();

            CreateMap<School, SchoolReadDto>();

            // Map events
            CreateMap<Common.Models.Program, ProgramCreated>();
            CreateMap<Common.Models.Program, ProgramDeleted>();
            CreateMap<Common.Models.Program, ProgramUpdated>();
        }
    }
}
