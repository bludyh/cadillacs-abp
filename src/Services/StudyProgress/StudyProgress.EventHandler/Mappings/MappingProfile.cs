using AutoMapper;
using Infrastructure.Common.Events;
using StudyProgress.Common.Models;

namespace StudyProgress.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SchoolCreated, School>();
            CreateMap<SchoolUpdated, School>();
            CreateMap<StudentCreated, Student>();
            CreateMap<StudentUpdated, Student>();
            CreateMap<CourseCreated, Course>();
            CreateMap<CourseUpdated, Course>();
            CreateMap<ClassCreated, Class>();
            CreateMap<ClassUpdated, Class>();
        }
    }
}
