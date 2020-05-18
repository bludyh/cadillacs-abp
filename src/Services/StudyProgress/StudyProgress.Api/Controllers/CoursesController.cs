using Microsoft.AspNetCore.Mvc;
using StudyProgress.Api.Dtos;
using StudyProgress.Api.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StudyProgress.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        #region Programs
        // GET: api/Courses/prc1/Programs
        [HttpGet("{id}/programs")]
        public async Task<ActionResult<IEnumerable<ProgramReadDto>>> GetPrograms([FromRoute] string id)
        {
            return await _courseService.GetProgramsAsync(id);
        }

        // POST: api/Courses/prc1/Programs
        [HttpPost("{id}/programs")]
        public async Task<ActionResult<ProgramReadDto>> AddProgram([FromRoute] string id, [FromBody, Required] string programId)
        {
            var program = await _courseService.AddProgramAsync(id, programId);

            return CreatedAtAction(nameof(GetPrograms), new { id }, program);
        }

        // DELETE: api/Courses/prc1/Programs/5
        [HttpDelete("{id}/programs/{programId}")]
        public async Task<ActionResult<ProgramReadDto>> RemoveProgram([FromRoute] string id, [FromRoute] string programId)
        {
            return await _courseService.RemoveProgramAsync(id, programId);
        }
        #endregion

        #region Requirements
        // GET: api/Courses/prc1/Requirements
        [HttpGet("{id}/requirements")]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetRequirements([FromRoute] string id)
        {
            return await _courseService.GetRequirementsAsync(id);
        }

        // POST: api/Courses/prc1/Requirements
        [HttpPost("{id}/requirements")]
        public async Task<ActionResult<CourseReadDto>> AddRequirement([FromRoute] string id, [FromBody, Required] string requiredCourseId)
        {
            var requiredCourse = await _courseService.AddRequirementAsync(id, requiredCourseId);

            return CreatedAtAction(nameof(GetRequirements), new { id }, requiredCourse);
        }

        // DELETE: api/Courses/prc1/Requirements/5
        [HttpDelete("{id}/requirements/{requiredCourseId}")]
        public async Task<ActionResult<CourseReadDto>> RemoveRequirement([FromRoute] string id, [FromRoute] string requiredCourseId)
        {
            return await _courseService.RemoveRequirementAsync(id, requiredCourseId);
        }
        #endregion

    }
}
