﻿using AutoMapper;
using Identity.Api.Dtos;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Mappings {
    public class MappingProfile : Profile {

        public MappingProfile() {
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

            CreateMap<Teacher, EmployeeReadDto>()
                .ForMember(dest => dest.Pcn, opt => opt.MapFrom(src => src.UserName));
            CreateMap<EmployeeUpdateDto, Teacher>(MemberList.Source);
            CreateMap<EmployeeCreateDto, Teacher>(MemberList.Source);

            CreateMap<Models.Program, ProgramDto>();

            CreateMap<Room, RoomDto>();

            CreateMap<Building, BuildingDto>();

            CreateMap<string, RoleDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src));
            CreateMap<IdentityRole<int>, RoleDto>();

            CreateMap<Mentor, StudentMentorReadDto>();
        }

    }
}
