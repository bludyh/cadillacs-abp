using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using Schedule.Api.Data;
using Schedule.Api.Dtos;
using Schedule.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Services
{
    public interface IClassService
    {
        public Task<List<ClassScheduleReadDto>> GetClassSchedulesAsync(string roomId, string classCourseId, string classId, int classSemester, int classYear);

        //public Task<ClassScheduleReadDto> AddClassScheduleAsync(string roomId, string classCourseId, string classId, int classSemester, int classYear, int studentId);

        //public Task<List<ClassEnrollmentReadDto>> GetEnrollmentsAsync(string courseId, string classId, int classSemester, int classYear);
        //public Task<ClassEnrollmentReadDto> AddEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId);
        // Task<ClassEnrollmentReadDto> RemoveEnrollmentAsync(string courseId, string classId, int classSemester, int classYear, int studentId);


    }

    public class ClassService : ServiceBase, IClassService
    {
        private readonly IMapper _mapper;

        public ClassService(ScheduleContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }


        public async Task<List<ClassScheduleReadDto>> GetClassSchedulesAsync(string roomId, string classCourseId, string classId, int classSemester, int classYear)
        {
            var room = await ValidateExistenceAsync<Room>(roomId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            
            //Not sure if I need validation here
            //await ValidateForeignKeyAsync<Class>(inputClass);

            await _context.Entry(inputClass)
                .Collection(c => c.ClassSchedules)
                .Query()
                .Include(e => e.Class)
                .Include(e => e.Room)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassScheduleReadDto>(inputClass.ClassSchedules.AsQueryable()).ToListAsyncFallback();
        }

        //public async Task<ClassScheduleReadDto> AddClassScheduleAsync(string roomId, string classCourseId, string classId, int classSemester, int classYear)
        //{
        //    //Validations
        //    var room = await ValidateExistenceAsync<Room>(roomId);
        //    var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            
        //    //await ValidateDuplicationAsync<ClassSchedule>(dto.ClassId, dto.ClassSemester, dto.ClassYear, dto.ClassCourseId, studentId);

        //    //Create the new object
        //    var schedule = _mapper.Map<ClassSchedule>();
        //    schedule.ClassId = classId;

        //    //Add the new obj to the db
        //    await _context.AddAsync(schedule);
        //    await _context.SaveChangesAsync();

        //    //Links the relevant properties and subclasses
        //    await _context.Entry(schedule).Reference(e => e.Class).LoadAsync();
        //    await _context.Entry(schedule).Reference(e => e.Room).LoadAsync();


        //    //Returns the readDto
        //    return _mapper.Map<ClassScheduleReadDto>(schedule);
        //}
    }
}
