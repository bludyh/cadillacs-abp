using AutoMapper;
using Schedule.Api.Dtos;
using Schedule.Common.Models;

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
