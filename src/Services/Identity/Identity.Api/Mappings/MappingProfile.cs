using AutoMapper;
using Identity.Api.Dtos;
using Identity.Common.Models;
using Infrastructure.Common.Events;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<School, SchoolCreateReadDto>().ReverseMap();
            CreateMap<SchoolUpdateDto, School>(MemberList.Source);

            CreateMap<Student, StudentReadDto>()
                .ForMember(dest => dest.Pcn, opt => opt.MapFrom(src => src.UserName));
            CreateMap<StudentCreateDto, Student>(MemberList.Source);
            CreateMap<StudentUpdateDto, Student>(MemberList.Source);

            CreateMap<Employee, EmployeeReadDto>()
                .ForMember(dest => dest.Pcn, opt => opt.MapFrom(src => src.UserName));
            CreateMap<EmployeeUpdateDto, Employee>(MemberList.Source);
            CreateMap<EmployeeCreateDto, Employee>(MemberList.Source);

            CreateMap<Common.Models.Program, ProgramReadDto>();

            CreateMap<Room, RoomReadDto>();

            CreateMap<Building, BuildingReadDto>();

            CreateMap<string, RoleReadDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
            CreateMap<IdentityRole<int>, RoleReadDto>();

            CreateMap<Mentor, StudentMentorReadDto>();
            CreateMap<Mentor, TeacherMentorReadDto>();

            // Map events
            CreateMap<School, SchoolCreated>();
            CreateMap<School, SchoolDeleted>();
            CreateMap<School, SchoolUpdated>();
            CreateMap<Student, StudentCreated>();
            CreateMap<Student, StudentDeleted>();
            CreateMap<Student, StudentUpdated>();
            CreateMap<Room, RoomCreated>();
            CreateMap<Room, RoomDeleted>();
            CreateMap<Employee, EmployeeCreated>();
            CreateMap<Employee, EmployeeDeleted>();
            CreateMap<Employee, EmployeeUpdated>();
        }

    }
}
