using Microsoft.AspNetCore.Mvc;
using Schedule.Api.Dtos;
using Schedule.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Controllers
{
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassesController(IClassService classService)
        {
            _classService = classService;
        }



        [HttpGet("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/schedules")]
        public async Task<ActionResult<IEnumerable<ClassScheduleReadDto>>> GetClassSchedules(string roomId, string classCourseId, string classId, int classSemester, int classYear
        {
            return await _classService.GetClassSchedulesAsync(roomId, classCourseId, classId, classSemester, classYear);
        }

    }
}
