using Schedule.Api.Models;
using Schedule.Api.Dtos;
using AutoMapper;
using Infrastructure.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassSchedule, ClassScheduleReadDto>();
            CreateMap<ClassClassScheduleCreateDto, ClassSchedule>(MemberList.Source);

            CreateMap<Class, ClassReadDto>();
            CreateMap<Room, RoomReadDto>();
            CreateMap<TimeSlot, TimeSlotReadDto>();
        }
    }
}
