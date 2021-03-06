using Course.Api.Dtos;
using Course.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<CourseReadDto>> PostCourse([FromBody] CourseCreateDto dto)
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
        {
            return await _courseService.DeleteClassAsync(classCourseId, classId, classSemester, classYear);
        }
        #endregion

        #region StudyMaterials
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials")]
        public async Task<ActionResult<IEnumerable<StudyMaterialReadDto>>> GetStudyMaterials(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
        {
            return await _courseService.GetStudyMaterialsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<ActionResult<StudyMaterialReadDto>> GetStudyMaterials(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId)
        {
            return await _courseService.GetStudyMaterialAsync(classCourseId, classId, classSemester, classYear, studyMaterialId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<IActionResult> PutStudyMaterial(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId,
            [FromBody] StudyMaterialCreateUpdateDto dto)
        {
            await _courseService.UpdateStudyMaterialAsync(classCourseId, classId, classSemester,
                classYear, studyMaterialId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials")]
        public async Task<ActionResult<StudyMaterialReadDto>> CreateStudyMaterial(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromBody] StudyMaterialCreateUpdateDto dto)
        {
            var studyMaterial = await _courseService.CreateStudyMaterialAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                dto);

            return CreatedAtAction(nameof(GetStudyMaterials), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear
            }, studyMaterial);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}")]
        public async Task<ActionResult<StudyMaterialReadDto>> DeleteStudyMaterial(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId)
        {
            return await _courseService.DeleteStudyMaterialAsync(
                classCourseId, classId, classSemester, classYear, studyMaterialId);
        }
        #endregion

        #region StudyMaterial/Attachments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments")]
        public async Task<ActionResult<IEnumerable<StudyMaterialAttachmentReadDto>>> GetStudyMaterialAttachments(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId)
        {
            return await _courseService.GetStudyMaterialAttachmentsAsync(classCourseId, classId, classSemester, classYear, studyMaterialId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments/{attachmentId}")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> GetStudyMaterialAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId,
            [FromRoute] int attachmentId)
        {
            return await _courseService.GetStudyMaterialAttachmentAsync(classCourseId, classId, classSemester, classYear, studyMaterialId, attachmentId);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Study-Materials/5/Attachments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> CreateStudyMaterialAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId,
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
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/study-materials/{studyMaterialId}/attachments/{attachmentId}")]
        public async Task<ActionResult<StudyMaterialAttachmentReadDto>> DeleteStudyMaterialAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studyMaterialId,
            [FromRoute] int attachmentId)
        {
            return await _courseService.DeleteStudyMaterialAttachmentAsync(
                classCourseId, classId, classSemester, classYear, studyMaterialId, attachmentId);
        }
        #endregion

        #region Enrollments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Enrollments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDto>>> GetEnrollments(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int studentId)
        {
            return await _courseService.DeleteEnrollmentAsync(
                classCourseId, classId, classSemester, classYear, studentId);
        }
        #endregion

        #region Assignments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments")]
        public async Task<ActionResult<IEnumerable<AssignmentReadDto>>> GetAssignments(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
        {
            return await _courseService.GetAssignmentsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}")]
        public async Task<ActionResult<AssignmentReadDto>> GetAssignment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId)
        {
            return await _courseService.GetAssignmentAsync(classCourseId, classId, classSemester, classYear, assignmentId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}")]
        public async Task<IActionResult> PutAssignment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromBody] AssignmentCreateUpdateDto dto)
        {
            await _courseService.UpdateAssignmentAsync(classCourseId, classId, classSemester,
                classYear, assignmentId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Assignments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments")]
        public async Task<ActionResult<AssignmentReadDto>> CreateAssignment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId)
        {
            return await _courseService.DeleteAssignmentAsync(
                classCourseId, classId, classSemester, classYear, assignmentId);
        }
        #endregion

        #region Assignments/StudentSubmissions
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Student-Submissions
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/student-submissions")]
        public async Task<ActionResult<IEnumerable<StudentSubmissionReadDto>>> GetAssignmentStudentSubmissions(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId)
        {
            return await _courseService.GetAssignmentStudentSubmissionsAsync(classCourseId, classId, classSemester, classYear, assignmentId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Student-Submissions/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/student-submissions/{submissionId}")]
        public async Task<ActionResult<StudentSubmissionReadDto>> GetAssignmentStudentSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int submissionId)
        {
            return await _courseService.GetAssignmentStudentSubmissionAsync(classCourseId, classId, classSemester, classYear, assignmentId, submissionId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Student-Submissions/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/student-submissions/{submissionId}")]
        public async Task<IActionResult> PutAssignmentStudentSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int submissionId,
            [FromBody] StudentSubmissionCreateUpdateDto dto)
        {
            await _courseService.UpdateAssignmentStudentSubmissionAsync(classCourseId, classId, classSemester,
                classYear, assignmentId, submissionId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Student-Submissions
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/student-submissions")]
        public async Task<ActionResult<StudentSubmissionReadDto>> CreateAssignmentStudentSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromBody] StudentSubmissionCreateUpdateDto dto)
        {
            var studentSubmission = await _courseService.CreateAssignmentStudentSubmissionAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId,
                dto);

            return CreatedAtAction(nameof(GetAssignmentStudentSubmissions), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId
            }, studentSubmission);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Student-Submissions/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/student-submissions/{submissionId}")]
        public async Task<ActionResult<StudentSubmissionReadDto>> DeleteAttachmentStudentSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int submissionId)
        {
            return await _courseService.DeleteAssignmentStudentSubmissionAsync(
                classCourseId, classId, classSemester, classYear, assignmentId, submissionId);
        }
        #endregion

        #region Assignments/GroupSubmissions
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Group-Submissions
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/group-submissions")]
        public async Task<ActionResult<IEnumerable<GroupSubmissionReadDto>>> GetAssignmentGroupSubmissions(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId)
        {
            return await _courseService.GetAssignmentGroupSubmissionsAsync(classCourseId, classId, classSemester, classYear, assignmentId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Group-Submissions/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/group-submissions/{submissionId}")]
        public async Task<ActionResult<GroupSubmissionReadDto>> GetAssignmentGroupSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int submissionId)
        {
            return await _courseService.GetAssignmentGroupSubmissionAsync(classCourseId, classId, classSemester, classYear, assignmentId, submissionId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Group-Submissions/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/group-submissions/{submissionId}")]
        public async Task<IActionResult> PutAssignmentGroupSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId, 
            [FromRoute] int submissionId,
            [FromBody] GroupSubmissionCreateUpdateDto dto)
        {
            await _courseService.UpdateAssignmentGroupSubmissionAsync(classCourseId, classId, classSemester,
                classYear, assignmentId, submissionId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Group-Submissions
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/group-submissions")]
        public async Task<ActionResult<GroupSubmissionReadDto>> CreateAssignmentGroupSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromBody] GroupSubmissionCreateUpdateDto dto)
        {
            var groupSubmission = await _courseService.CreateAssignmentGroupSubmissionAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId,
                dto);

            return CreatedAtAction(nameof(GetAssignmentGroupSubmissions), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId
            }, groupSubmission);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Group-Submissions/5
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/group-submissions/{submissionId}")]
        public async Task<ActionResult<GroupSubmissionReadDto>> DeleteAttachmentGroupSubmission(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int submissionId)
        {
            return await _courseService.DeleteAssignmentGroupSubmissionAsync(
                classCourseId, classId, classSemester, classYear, assignmentId, submissionId);
        }
        #endregion

        #region Assignments/Attachments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Attachments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/attachments")]
        public async Task<ActionResult<IEnumerable<AssignmentAttachmentReadDto>>> GetAssignmentAttachments(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId)
        {
            return await _courseService.GetAssignmentAttachmentsAsync(classCourseId, classId, classSemester, classYear, assignmentId);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Attachments/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/attachments/{attachmentId}")]
        public async Task<ActionResult<AssignmentAttachmentReadDto>> GetAssignmentAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int attachmentId)
        {
            return await _courseService.GetAssignmentAttachmentAsync(classCourseId, classId, classSemester, classYear, assignmentId, attachmentId);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Attachments
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/attachments")]
        public async Task<ActionResult<AssignmentAttachmentReadDto>> CreateAssignmentAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromBody] AssignmentAttachmentCreateDto dto)
        {
            var assignmentAttachment = await _courseService.CreateAssignmentAttachmentAsync(
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId,
                dto);

            return CreatedAtAction(nameof(GetAssignmentAttachments), new
            {
                classCourseId,
                classId,
                classSemester,
                classYear,
                assignmentId
            }, assignmentAttachment);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Assignments/5/Attachments
        [HttpDelete("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/assignments/{assignmentId}/attachments/{attachmentId}")]
        public async Task<ActionResult<AssignmentAttachmentReadDto>> DeleteAttachmentAttachment(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int assignmentId,
            [FromRoute] int attachmentId)
        {
            return await _courseService.DeleteAssignmentAttachmentAsync(
                classCourseId, classId, classSemester, classYear, assignmentId, attachmentId);
        }
        #endregion

        #region Groups
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups")]
        public async Task<ActionResult<IEnumerable<GroupReadDto>>> GetGroups(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
        {
            return await _courseService.GetGroupsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups/5
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}")]
        public async Task<ActionResult<GroupReadDto>> GetGroup(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int groupId)
        {
            return await _courseService.GetGroupAsync(classCourseId, classId, classSemester, classYear, groupId);
        }

        // PUT: api/Courses/prc1/Classes/e-s71/1/2020/Groups/5
        [HttpPut("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}")]
        public async Task<IActionResult> PutGroup(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int groupId,
            [FromBody] GroupCreateUpdateDto dto)
        {
            await _courseService.UpdateGroupAsync(classCourseId, classId, classSemester,
                classYear, groupId, dto);

            return NoContent();
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Groups
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups")]
        public async Task<ActionResult<GroupReadDto>> CreateGroup(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int groupId)
        {
            return await _courseService.DeleteGroupAsync(
                classCourseId, classId, classSemester, classYear, groupId);
        }
        #endregion

        #region Groups/Enrollments
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Groups/{id}/Enrollments
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/groups/{groupId}/enrollments")]
        public async Task<ActionResult<IEnumerable<EnrollmentReadDto>>> GetGroupEnrollments(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int groupId)
        {
            return await _courseService.GetGroupEnrollmentsAsync(classCourseId, classId, classSemester, classYear, groupId);
        }
        #endregion

        #region Lecturers
        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Lecturers
        [HttpGet("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/lecturers")]
        public async Task<ActionResult<IEnumerable<LecturerReadDto>>> GetLecturers(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear)
        {
            return await _courseService.GetLecturersAsync(classCourseId, classId, classSemester, classYear);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Lecturers
        [HttpPost("{classCourseId}/classes/{classId}/{classSemester}/{classYear}/lecturers")]
        public async Task<ActionResult<LecturerReadDto>> CreateLecturer(
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
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
            [FromRoute] string classCourseId,
            [FromRoute] string classId,
            [FromRoute] int classSemester,
            [FromRoute] int classYear,
            [FromRoute] int teacherId)
        {
            return await _courseService.DeleteLecturerAsync(
                classCourseId, classId, classSemester, classYear, teacherId);
        }
        #endregion
    }
}