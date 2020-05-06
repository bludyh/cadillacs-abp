using AutoMapper;
using StudyProgress.EventHandler.Events;
using StudyProgress.EventHandler.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProgress.EventHandler.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SchoolCreated, School>(MemberList.Source);
            CreateMap<SchoolUpdated, School>(MemberList.Source);
            CreateMap<StudentCreated, Student>(MemberList.Source);
            CreateMap<StudentUpdated, Student>(MemberList.Source);
        }
    }
}
