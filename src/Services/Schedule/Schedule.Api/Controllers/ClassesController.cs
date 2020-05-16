using Microsoft.AspNetCore.Mvc;
using Schedule.Api.Dtos;
using Schedule.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/schedules")]
        public async Task<ActionResult<IEnumerable<ClassScheduleReadDto>>> GetClassSchedules(string classCourseId, string classId, int classSemester, int classYear)
        {
            return await _classService.GetClassSchedulesAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Schedules
        [HttpPost("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/schedules")]
        public async Task<ActionResult<ClassScheduleReadDto>> AddClassSchedules(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleCreateDto dto)
        {
            var classSchedule = await _classService.AddClassScheduleAsync(classCourseId, classId, classSemester, classYear, dto);

            return CreatedAtAction(nameof(GetClassSchedules), new { classCourseId, classId, classSemester, classYear }, classSchedule);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Schedules
        [HttpDelete("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/schedules")]
        public async Task<ActionResult<ClassScheduleReadDto>> RemoveClassSchedule(string classCourseId, string classId, int classSemester, int classYear, ClassClassScheduleDeleteDto dto)
        {
            return await _classService.RemoveClassScheduleAsync(classCourseId, classId, classSemester, classYear, dto);
        }
    }
}
