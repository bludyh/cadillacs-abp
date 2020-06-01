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
        public async Task<ActionResult<StudyMaterialReadDto>> GetStudyMaterial(
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

        #region StudyMaterial/Attachments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments")]
        public async Task<ActionResult<IEnumerable<StudyMaterialAttachmentReadDto>>> GetStudyMaterialAttachments(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId)
        {
            return await _courseService.GetStudyMaterialAttachmentsAsync(classCourseId, classId, classSemester, classYear, studyMaterialId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments/{attachmentId}")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> GetStudyMaterialAttachment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId,
            int attachmentId)
        {
            return await _courseService.GetStudyMaterialAttachmentAsync(classCourseId, classId, classSemester, classYear, studyMaterialId, attachmentId);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> CreateStudyMaterialAttachment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId,
            [FromBody] StudyMaterialAttachmentCreateDto dto)
        {
            var studyMaterialAttachment = await _courseService.CreateStudyMaterialAttachmentAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                studyMaterialId,
                dto);

            return CreatedAtAction(nameof(GetStudyMaterialAttachments), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear,
                studyMaterialId
            }, studyMaterialAttachment);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> DeleteStudyMaterialAttachment(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int studyMaterialId,
            [FromQuery] int attachmentId)
        {
            return await _courseService.DeleteStudyMaterialAttachmentAsync(
                classCourseId, classId, classSemester, classYear, studyMaterialId, attachmentId);
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

        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/enrollments/{studentId}")]
        public async Task<IActionResult> PutEnrollment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studentId,
            [FromBody] EnrollmentUpdateDto dto)
        {
            await _courseService.UpdateEnrollmentAsync(classCourseId, classId, classSemester, classYear, studentId, dto);

            return NoContent();
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

        #region Groups
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups")]
        public async Task<ActionResult<IEnumerable<GroupReadDto>>> GetGroups(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetGroupsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}")]
        public async Task<ActionResult<GroupReadDto>> GetGroup(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int groupId)
        {
            return await _courseService.GetGroupAsync(classCourseId, classId, classSemester, classYear, groupId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Groups/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}")]
        public async Task<IActionResult> PutGroup(string classCourseId, string classId,
            int classSemester, int classYear, int groupId, [FromBody] GroupCreateUpdateDto dto)
        {
            await _courseService.UpdateGroupAsync(classCourseId, classId, classSemester,
                classYear, groupId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Groups
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups")]
        public async Task<ActionResult<GroupReadDto>> CreateGroup(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            [FromBody] GroupCreateUpdateDto dto)
        {
            var group = await _courseService.CreateGroupAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                dto);

            return CreatedAtAction(nameof(GetGroups), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, group);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Groups/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}")]
        public async Task<ActionResult<GroupReadDto>> DeleteGroup(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int groupId)
        {
            return await _courseService.DeleteGroupAsync(
                classCourseId, classId, classSemester, classYear, groupId);
        }
        #endregion

        #region Groups/Enrollments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups/{id}/Enrollments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}/enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDto>>> GetGroupEnrollments(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int groupId)
        {
            return await _courseService.GetGroupEnrollmentsAsync(classCourseId, classId, classSemester, classYear, groupId);
        }
        #endregion

        #region Lecturers
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Lecturers
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/lecturers")]
        public async Task<ActionResult<IEnumerable<LecturerReadDto>>> GetLecturers(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear)
        {
            return await _courseService.GetLecturersAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Lecturers
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/lecturers")]
        public async Task<ActionResult<LecturerReadDto>> CreateLecturer(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            [FromBody] ClassLecturerCreateDto dto)
        {
            var lecturer = await _courseService.CreateLecturerAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                dto);

            return CreatedAtAction(nameof(GetLecturers), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, lecturer);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Lecturers/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/Lecturers/{teacherId}")]
        public async Task<ActionResult<LecturerReadDto>> DeleteLecturer(
            string classCourseId,
            string classId,
            int classSemester,
            int classYear,
            int teacherId)
        {
            return await _courseService.DeleteLecturerAsync(
                classCourseId, classId, classSemester, classYear, teacherId);
        }
        #endregion
    }
}