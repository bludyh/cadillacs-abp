using AutoMapper;
using Infrastructure.Common.Events;
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
            CreateMap<SchoolCreated, School>();
            CreateMap<SchoolUpdated, School>();
            CreateMap<StudentCreated, Student>();
            CreateMap<StudentUpdated, Student>();
        }
    }
}
