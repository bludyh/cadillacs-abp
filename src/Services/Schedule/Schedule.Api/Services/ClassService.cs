using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using Schedule.Api.Data;
using Schedule.Api.Dtos;
using Schedule.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Services
{
    public interface IClassService
    {
        public Task<List<ClassScheduleReadDto>> GetClassSchedulesAsync(string classCourseId, string classId, int classSemester, int classYear);
        public Task<ClassScheduleReadDto> AddClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDeleteDto dto);
        public Task<ClassScheduleReadDto> RemoveClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDeleteDto dto);
    }

    public class ClassService : ServiceBase, IClassService
    {
        private readonly IMapper _mapper;

        public ClassService(ScheduleContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<ClassScheduleReadDto>> GetClassSchedulesAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.ClassSchedules)
                .Query()
                .Include(cs => cs.Room)
                .Include(cs => cs.TimeSlot)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassScheduleReadDto>(inputClass.ClassSchedules.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassScheduleReadDto> AddClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDeleteDto dto)
        {
            //Validations
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<TimeSlot>(dto.TimeSlotId);
            await ValidateExistenceAsync<Room>(dto.RoomId, dto.RoomBuildingId);

            await ValidateDuplicationAsync<ClassSchedule>(dto.TimeSlotId, dto.Date, dto.RoomId, dto.RoomBuildingId);

            //Create the new object
            var schedule = _mapper.Map<ClassSchedule>(dto);
            schedule.Class = inputClass;

            //Add the new obj to the db
            await _context.AddAsync(schedule);
            await _context.SaveChangesAsync();

            //Links the relevant properties and subclasses
            await _context.Entry(schedule).Reference(e => e.TimeSlot).LoadAsync();
            await _context.Entry(schedule).Reference(e => e.Room).LoadAsync();
            await _context.Entry(schedule).Reference(e => e.Class).LoadAsync();

            //Returns the readDto
            return _mapper.Map<ClassScheduleReadDto>(schedule);
        }

        public async Task<ClassScheduleReadDto> RemoveClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDeleteDto dto)
        {
            //Validations
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<TimeSlot>(dto.TimeSlotId);
            await ValidateExistenceAsync<Room>(dto.RoomId, dto.RoomBuildingId);

            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var classSchedule = await ValidateExistenceAsync<ClassSchedule>(dto.TimeSlotId, dto.Date, dto.RoomId, dto.RoomBuildingId);

            _context.Remove(classSchedule);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassScheduleReadDto>(classSchedule);
        }
    }
}
