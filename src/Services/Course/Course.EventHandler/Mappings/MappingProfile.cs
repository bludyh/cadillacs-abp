using AutoMapper;
using Course.Common.Models;
using Infrastructure.Common.Events;

namespace Course.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentCreated, Student>();
            CreateMap<StudentUpdated, Student>();
            CreateMap<EmployeeCreated, Teacher>();
            CreateMap<EmployeeUpdated, Teacher>();
        }
    }
}
