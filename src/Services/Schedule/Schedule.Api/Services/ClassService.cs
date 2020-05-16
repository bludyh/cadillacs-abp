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
        public Task<ClassScheduleReadDto> AddClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDto dto);
        public Task<ClassScheduleReadDto> RemoveClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleDeleteDto dto);
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
                .Include(e => e.Class)
                .Include(e => e.Room)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassScheduleReadDto>(inputClass.ClassSchedules.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassScheduleReadDto> AddClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDto dto)
        {
            //Validations
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var room = await ValidateExistenceAsync<Room>(dto.RoomId, dto.RoomBuildingId);

            await ValidateDuplicationAsync<ClassSchedule>(dto.StartTime, classId, classSemester, classYear, classCourseId, dto.RoomId, dto.RoomBuildingId);

            //Create the new object
            var schedule = new ClassSchedule
            {
                StartTime = dto.StartTime,
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                RoomId = dto.RoomId,
                RoomBuildingId = dto.RoomBuildingId,
                EndTime = dto.EndTime
            };

            //Add the new obj to the db
            await _context.AddAsync(schedule);
            await _context.SaveChangesAsync();

            //Links the relevant properties and subclasses
            await _context.Entry(schedule).Reference(e => e.Class).LoadAsync();
            await _context.Entry(schedule).Reference(e => e.Room).LoadAsync();

            //Returns the readDto
            return _mapper.Map<ClassScheduleReadDto>(schedule);
        }

        public async Task<ClassScheduleReadDto> RemoveClassScheduleAsync(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleDeleteDto dto)
        {
            //Validations
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Room>(dto.RoomId, dto.RoomBuildingId);
            var classSchedule = await ValidateExistenceAsync<ClassSchedule>(dto.StartTime, classId, classSemester, classYear, classCourseId, dto.RoomId, dto.RoomBuildingId);

            _context.Remove(classSchedule);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassScheduleReadDto>(classSchedule);
        }
    }
}
