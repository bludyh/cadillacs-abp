using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Course.Api.Dtos;
using Course.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Api.Controllers
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

        #region Courses
        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses()
        {
            return await _courseService.GetAllAsync();
        }

        // GET: api/Courses/prc1
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReadDto>> GetCourse([FromRoute] string id)
        {
            return await _courseService.GetAsync(id);
        }

        // PUT: api/Courses/prc1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse([FromRoute] string id, [FromBody] CourseUpdateDto dto)
        {
            await _courseService.UpdateAsync(id, dto);

            return NoContent();
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> PostCourse(CourseCreateDto dto)
        {
            var course = await _courseService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/prc1
        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseReadDto>> DeleteCourse([FromRoute] string id)
        {
            return await _courseService.DeleteAsync(id);
        }
        #endregion

        #region Classes
        // GET: api/Courses/prc1/Classes
        [HttpGet("{classCourseId}/classes")]
        public async Task<ActionResult<IEnumerable<ClassReadDto>>> GetClasses([FromRoute] string classCourseId)
        {
            return await _courseService.GetClassesAsync(classCourseId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}")]
        public async Task<ActionResult<ClassReadDto>> GetClass(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetClassAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes
        [HttpPost("{classCourseId}/classes")]
        public async Task<ActionResult<ClassReadDto>> CreateClass(
            [FromRoute] string classCourseId,
            [FromBody] ClassCreateDto dto)
        {
            var inputClass = await _courseService.CreateClassAsync(classCourseId, dto);

            return CreatedAtAction(nameof(GetClasses), new { classCourseId }, inputClass);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}")]
        public async Task<ActionResult<ClassReadDto>> DeleteClass(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.DeleteClassAsync(classCourseId, classId, classSemester, classYear);
        }
        #endregion

        #region StudyMaterials
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials")]
        public async Task<ActionResult<IEnumerable<StudyMaterialReadDto>>> GetStudyMaterials(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetStudyMaterialsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<ActionResult<StudyMaterialReadDto>> GetStudyMaterials(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId)
        {
            return await _courseService.GetStudyMaterialAsync(classCourseId, classId, classSemester, classYear, studyMaterialId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<IActionResult> PutStudyMaterial(string classCourseId, string classId, 
            int classSemester, int classYear, int studyMaterialId, [FromBody] StudyMaterialCreateUpdateDto dto)
        {
            await _courseService.UpdateStudyMaterialAsync(classCourseId, classId, classSemester, 
                classYear, studyMaterialId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials")]
        public async Task<ActionResult<StudyMaterialReadDto>> CreateStudyMaterial(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            [FromBody] StudyMaterialCreateUpdateDto dto)
        {
            var studyMaterial = await _courseService.CreateStudyMaterialAsync(
                classCourseId,
                classId,
                classSemester,
                classYear, 
                dto);

            return CreatedAtAction(nameof(GetStudyMaterials), new {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, studyMaterial);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<ActionResult<StudyMaterialReadDto>> DeleteStudyMaterial(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId)
        {
            return await _courseService.DeleteStudyMaterialAsync(
                classCourseId, classId, classSemester, classYear, studyMaterialId);
        }
        #endregion

        #region Enrollments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Enrollments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDto>>> GetEnrollments(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetEnrollmentsAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Enrollments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/enrollments")]
        public async Task<ActionResult<EnrollmentReadDto>> CreateEnrollment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            [FromBody] EnrollmentCreateDto dto)
        {
            var enrollment = await _courseService.CreateEnrollmentAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                dto);

            return CreatedAtAction(nameof(GetEnrollments), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, enrollment);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Enrollments/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/enrollments/{studentId}")]
        public async Task<ActionResult<EnrollmentReadDto>> DeleteEnrollment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studentId)
        {
            return await _courseService.DeleteEnrollmentAsync(
                classCourseId, classId, classSemester, classYear, studentId);
        }
        #endregion

        #region Assignments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments")]
        public async Task<ActionResult<IEnumerable<AssignmentReadDto>>> GetAssignments(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetAssignmentsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}")]
        public async Task<ActionResult<AssignmentReadDto>> GetAssignment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int assignmentId)
        {
            return await _courseService.GetAssignmentAsync(classCourseId, classId, classSemester, classYear, assignmentId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}")]
        public async Task<IActionResult> PutAssignment(string classCourseId, string classId,
            int classSemester, int classYear, int assignmentId, [FromBody] AssignmentCreateUpdateDto dto)
        {
            await _courseService.UpdateAssignmentAsync(classCourseId, classId, classSemester,
                classYear, assignmentId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Assignments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments")]
        public async Task<ActionResult<AssignmentReadDto>> CreateAssignment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            [FromBody] AssignmentCreateUpdateDto dto)
        {
            var assignment = await _courseService.CreateAssignmentAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                dto);

            return CreatedAtAction(nameof(GetAssignments), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, assignment);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}")]
        public async Task<ActionResult<AssignmentReadDto>> DeleteAssignment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int assignmentId)
        {
            return await _courseService.DeleteAssignmentAsync(
                classCourseId, classId, classSemester, classYear, assignmentId);
        }
        #endregion
    }
}