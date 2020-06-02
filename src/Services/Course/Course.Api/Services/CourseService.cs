using AutoMapper;
using Course.Api.Dtos;
using Course.Common.Data;
using Course.Common.Models;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Services
{
    public interface ICourseService
    {
        #region Courses
        public Task<List<CourseReadDto>> GetAllAsync();
        public Task<CourseReadDto> GetAsync(string courseId);
        public Task UpdateAsync(string courseId, CourseUpdateDto dto);
        public Task<CourseReadDto> CreateAsync(CourseCreateDto dto);
        public Task<CourseReadDto> DeleteAsync(string courseId);
        #endregion

        #region Classes
        public Task<List<ClassReadDto>> GetClassesAsync(string classCourseId);
        public Task<ClassReadDto> GetClassAsync(string classCourseId, string classId, int classSemester, int classYear);
        public Task<ClassReadDto> CreateClassAsync(string classCourseId, ClassCreateDto dto);
        public Task<ClassReadDto> DeleteClassAsync(string courseId, string classId, int classSemester, int classYear);
        #endregion

        #region StudyMaterials
        public Task<List<StudyMaterialReadDto>> GetStudyMaterialsAsync(string classCourseId, 
            string classId, int classSemester, int classYear);
        public Task<StudyMaterialReadDto> GetStudyMaterialAsync(string classCourseId, 
            string classId, int classSemester, int classYear, int studyMaterialId);
        public Task UpdateStudyMaterialAsync(string classCourseId, string classId, int classSemester,
            int classYear, int studyMaterialId, StudyMaterialCreateUpdateDto dto);
        public Task<StudyMaterialReadDto> CreateStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, StudyMaterialCreateUpdateDto dto);
        public Task<StudyMaterialReadDto> DeleteStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId);
        #endregion

        #region StudyMaterials/Attachments
        public Task<List<StudyMaterialAttachmentReadDto>> GetStudyMaterialAttachmentsAsync(string classCourseId,
           string classId, int classSemester, int classYear, int studyMaterialId);
        public Task<StudyMaterialAttachmentReadDto> GetStudyMaterialAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId, int attachmentId);
        public Task<StudyMaterialAttachmentReadDto> CreateStudyMaterialAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId, StudyMaterialAttachmentCreateDto dto);
        public Task<StudyMaterialAttachmentReadDto> DeleteStudyMaterialAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId, int attachmentId);
        #endregion

        #region Enrollments
        public Task<List<EnrollmentReadDto>> GetEnrollmentsAsync(string classCourseId,
            string classId, int classSemester, int classYear);
        public Task UpdateEnrollmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studentId, EnrollmentUpdateDto dto);
        public Task<EnrollmentReadDto> CreateEnrollmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, EnrollmentCreateDto dto);
        public Task<EnrollmentReadDto> DeleteEnrollmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studentId);
        #endregion

        #region Assignments
        public Task<List<AssignmentReadDto>> GetAssignmentsAsync(string classCourseId,
           string classId, int classSemester, int classYear);
        public Task<AssignmentReadDto> GetAssignmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId);
        public Task UpdateAssignmentAsync(string classCourseId, string classId, int classSemester,
            int classYear, int assignmentId, AssignmentCreateUpdateDto dto);
        public Task<AssignmentReadDto> CreateAssignmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, AssignmentCreateUpdateDto dto);
        public Task<AssignmentReadDto> DeleteAssignmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId);
        #endregion

        #region Assignments/StudentSubmissions
        public Task<List<StudentSubmissionReadDto>> GetAssignmentStudentSubmissionsAsync(string classCourseId,
           string classId, int classSemester, int classYear, int assignmentId);
        public Task<StudentSubmissionReadDto> GetAssignmentStudentSubmissionAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, int submissionId);
        public Task UpdateAssignmentStudentSubmissionAsync(string classCourseId, string classId, int classSemester,
            int classYear, int assignmentId, int submissionId, StudentSubmissionCreateUpdateDto dto);
        public Task<StudentSubmissionReadDto> CreateAssignmentStudentSubmissionAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, StudentSubmissionCreateUpdateDto dto);
        public Task<StudentSubmissionReadDto> DeleteAssignmentStudentSubmissionAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, int submissionId);
        #endregion

        #region Assignments/Attachments
        public Task<List<AssignmentAttachmentReadDto>> GetAssignmentAttachmentsAsync(string classCourseId,
           string classId, int classSemester, int classYear, int assignmentId);
        public Task<AssignmentAttachmentReadDto> GetAssignmentAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, int attachmentId);
        public Task<AssignmentAttachmentReadDto> CreateAssignmentAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, AssignmentAttachmentCreateDto dto);
        public Task<AssignmentAttachmentReadDto> DeleteAssignmentAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, int attachmentId);
        #endregion

        #region Groups
        public Task<List<GroupReadDto>> GetGroupsAsync(string classCourseId,
           string classId, int classSemester, int classYear);
        public Task<GroupReadDto> GetGroupAsync(string classCourseId,
            string classId, int classSemester, int classYear, int groupId);
        public Task UpdateGroupAsync(string classCourseId, string classId, int classSemester,
            int classYear, int groupId, GroupCreateUpdateDto dto);
        public Task<GroupReadDto> CreateGroupAsync(string classCourseId,
            string classId, int classSemester, int classYear, GroupCreateUpdateDto dto);
        public Task<GroupReadDto> DeleteGroupAsync(string classCourseId,
            string classId, int classSemester, int classYear, int groupId);
        #endregion

        #region Groups/Enrollments
        public Task<List<EnrollmentReadDto>> GetGroupEnrollmentsAsync(string classCourseId,
            string classId, int classSemester, int classYear, int groupId);
        #endregion

        #region Lecturers
        public Task<List<LecturerReadDto>> GetLecturersAsync(string classCourseId,
            string classId, int classSemester, int classYear);
        public Task<LecturerReadDto> CreateLecturerAsync(string classCourseId,
            string classId, int classSemester, int classYear, ClassLecturerCreateDto dto);
        public Task<LecturerReadDto> DeleteLecturerAsync(string classCourseId,
            string classId, int classSemester, int classYear, int teacherId);
        #endregion
    }

    public class CourseService<T> : ServiceBase, ICourseService where T : Common.Models.Course
    {
        private readonly IMapper _mapper;

        public CourseService(CourseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region Courses
        public async Task<List<CourseReadDto>> GetAllAsync()
        {
            return await _mapper.ProjectTo<CourseReadDto>(_context.Set<T>()).ToListAsyncFallback();
        }

        public async Task<CourseReadDto> GetAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task UpdateAsync(string courseId, CourseUpdateDto dto)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            _mapper.Map(dto, course);
            _context.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReadDto> CreateAsync(CourseCreateDto dto)
        {
            await ValidateDuplicationAsync<T>(dto.Id);

            var course = _mapper.Map<T>(dto);

            await _context.AddAsync(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }

        public async Task<CourseReadDto> DeleteAsync(string courseId)
        {
            var course = await ValidateExistenceAsync<T>(courseId);

            _context.Remove(course);
            await _context.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>(course);
        }
        #endregion

        #region Classes
        public async Task<List<ClassReadDto>> GetClassesAsync(string classCourseId)
        {
            var course = await ValidateExistenceAsync<T>(classCourseId);

            await _context.Entry(course)
                .Collection(c => c.Classes)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassReadDto>(course.Classes.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassReadDto> GetClassAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }

        public async Task<ClassReadDto> CreateClassAsync(string classCourseId, ClassCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);

            await ValidateDuplicationAsync<Class>(dto.Id, dto.Semester, dto.Year, classCourseId);

            var inputClass = new Class 
            { 
                Id = dto.Id, 
                Semester = (int) dto.Semester, 
                Year = (int) dto.Year, 
                CourseId = classCourseId 
            };

            await _context.AddAsync(inputClass);
            await _context.SaveChangesAsync();

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }

        public async Task<ClassReadDto> DeleteClassAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Reference(c => c.Course)
                .LoadAsync();

            _context.Remove(inputClass);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassReadDto>(inputClass);
        }
        #endregion

        #region StudyMaterials
        public async Task<List<StudyMaterialReadDto>> GetStudyMaterialsAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.StudyMaterials)
                .Query()
                .Include(sm => sm.Class)
                .LoadAsync();

            return await _mapper.ProjectTo<StudyMaterialReadDto>(
                inputClass.StudyMaterials
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<StudyMaterialReadDto> GetStudyMaterialAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(studyMaterial)
                .Reference(sm => sm.Class)
                .LoadAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }

        public async Task UpdateStudyMaterialAsync(
            string classCourseId, string classId, int classSemester,
            int classYear, int studyMaterialId, StudyMaterialCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _mapper.Map(dto, studyMaterial);

            _context.Update(studyMaterial);
            await _context.SaveChangesAsync();
        }

        public async Task<StudyMaterialReadDto> CreateStudyMaterialAsync(string classCourseId,
            string classId, int classSemester, int classYear, StudyMaterialCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var studyMaterial = new StudyMaterial
            {
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                Name = dto.Name,
                Description = dto.Description,
                Week = dto.Week
            };

            await _context.AddAsync(studyMaterial);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }

        public async Task<StudyMaterialReadDto> DeleteStudyMaterialAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(studyMaterial)
                .Reference(sm => sm.Class)
                .Query()
                .Include(c => c.Course)
                .LoadAsync();

            _context.Remove(studyMaterial);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialReadDto>(studyMaterial);
        }
        #endregion

        #region StudyMaterials/Attachments
        public async Task<List<StudyMaterialAttachmentReadDto>> GetStudyMaterialAttachmentsAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var studyMaterial = await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<StudyMaterial>(studyMaterialId);

            await _context.Entry(studyMaterial)
                .Collection(sm => sm.StudyMaterialAttachments)
                .Query()
                .Include(sm => sm.Attachment)
                .LoadAsync();

            return await _mapper.ProjectTo<StudyMaterialAttachmentReadDto>(
                studyMaterial.StudyMaterialAttachments
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<StudyMaterialAttachmentReadDto> GetStudyMaterialAttachmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId, int attachmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateExistenceAsync<Attachment>(attachmentId);
            var studyMaterialAttachment = await ValidateExistenceAsync<StudyMaterialAttachment>(studyMaterialId, attachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Attachment>(attachmentId);

            await _context.Entry(studyMaterialAttachment)
                .Reference(sma => sma.Attachment)
                .LoadAsync();

            return _mapper.Map<StudyMaterialAttachmentReadDto>(studyMaterialAttachment);
        }

        public async Task<StudyMaterialAttachmentReadDto> CreateStudyMaterialAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studyMaterialId, StudyMaterialAttachmentCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateExistenceAsync<Attachment>(dto.AttachmentId);
            await ValidateDuplicationAsync<StudyMaterialAttachment>(studyMaterialId, dto.AttachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var studyMaterialAttachment = new StudyMaterialAttachment
            {
                StudyMaterialId = studyMaterialId,
                AttachmentId = (int)dto.AttachmentId
            };

            await _context.AddAsync(studyMaterialAttachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialAttachmentReadDto>(studyMaterialAttachment);
        }

        public async Task<StudyMaterialAttachmentReadDto> DeleteStudyMaterialAttachmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studyMaterialId, int attachmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<StudyMaterial>(studyMaterialId);
            await ValidateExistenceAsync<Attachment>(attachmentId);
            var studyMaterialAttachment = await ValidateExistenceAsync<StudyMaterialAttachment>(studyMaterialId, attachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<StudyMaterial>(studyMaterialId);
            await ValidateForeignKeyAsync<Attachment>(attachmentId);

            _context.Remove(studyMaterialAttachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudyMaterialAttachmentReadDto>(studyMaterialAttachment);
        }
        #endregion

        #region Enrollments
        public async Task<List<EnrollmentReadDto>> GetEnrollmentsAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.Enrollments)
                .Query()
                .Include(e => e.Student)
                .Include(e => e.Group)
                .LoadAsync();

            return await _mapper.ProjectTo<EnrollmentReadDto>(
                inputClass.Enrollments
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task UpdateEnrollmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int studentId, EnrollmentUpdateDto dto)
        {
            var enrollment = await ValidateExistenceAsync<Enrollment>(studentId, classId, classSemester, classYear, classCourseId);

            _mapper.Map(dto, enrollment);
            _context.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task<EnrollmentReadDto> CreateEnrollmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, EnrollmentCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateDuplicationAsync<Enrollment>(dto.StudentId, classId, classSemester, classYear, classCourseId);

            var enrollment = new Enrollment
            {
                StudentId = (int)dto.StudentId, 
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                GroupId = (int)dto.GroupId
            };

            await _context.AddAsync(enrollment);
            await _context.SaveChangesAsync();

            return _mapper.Map<EnrollmentReadDto>(enrollment);
        }

        public async Task<EnrollmentReadDto> DeleteEnrollmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int studentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var enrollment = await ValidateExistenceAsync<Enrollment>(
                studentId, classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(enrollment)
                .Reference(e => e.Class)
                .Query()
                .Include(c => c.Course)
                .LoadAsync();

            await _context.Entry(enrollment)
                .Reference(e => e.Group)
                .LoadAsync();

            await _context.Entry(enrollment)
                .Reference(e => e.Student)
                .LoadAsync();

            _context.Remove(enrollment);
            await _context.SaveChangesAsync();

            return _mapper.Map<EnrollmentReadDto>(enrollment);
        }
        #endregion

        #region Assignments
        public async Task<List<AssignmentReadDto>> GetAssignmentsAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.Assignments)
                .Query()
                .Include(a => a.Class)
                .LoadAsync();

            return await _mapper.ProjectTo<AssignmentReadDto>(
                inputClass.Assignments
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<AssignmentReadDto> GetAssignmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(assignment)
                .Reference(a => a.Class)
                .LoadAsync();

            return _mapper.Map<AssignmentReadDto>(assignment);
        }

        public async Task UpdateAssignmentAsync(
            string classCourseId, string classId, int classSemester,
            int classYear, int assignmentId, AssignmentCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _mapper.Map(dto, assignment);

            _context.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<AssignmentReadDto> CreateAssignmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, AssignmentCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var assignment = new Assignment
            {
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                Type = dto.Type,
                Description = dto.Description,
                DeadlineDateTime = dto.DeadlineDateTime,
                Weight = (int)dto.Weight
            };

            await _context.AddAsync(assignment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssignmentReadDto>(assignment);
        }

        public async Task<AssignmentReadDto> DeleteAssignmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(assignment)
                .Reference(a => a.Class)
                .LoadAsync();

            _context.Remove(assignment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssignmentReadDto>(assignment);
        }
        #endregion

        #region Assignments/StudentSubmissions
        public async Task<List<StudentSubmissionReadDto>> GetAssignmentStudentSubmissionsAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);

            await _context.Entry(assignment)
                .Collection(a => a.Submissions)
                .Query()
                .OfType<StudentSubmission>()
                .Include(s => s.Attachment)
                .Include(s => s.Student)
                .LoadAsync();

            return await _mapper.ProjectTo<StudentSubmissionReadDto>(
                assignment.Submissions.OfType<StudentSubmission>()
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<StudentSubmissionReadDto> GetAssignmentStudentSubmissionAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId, int submissionId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            var studentSubmission = await ValidateExistenceAsync<StudentSubmission>(submissionId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<StudentSubmission>(submissionId);

            await _context.Entry(assignment)
                .Collection(a => a.Submissions)
                .Query()
                .OfType<StudentSubmission>()
                .Include(ss => ss.Student)
                .Include(ss => ss.Attachment)
                .LoadAsync();

            return _mapper.Map<StudentSubmissionReadDto>(studentSubmission);
        }

        public async Task UpdateAssignmentStudentSubmissionAsync(
            string classCourseId, string classId, int classSemester,
            int classYear, int assignmentId, int submissionId, StudentSubmissionCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            var studentSubmission = await ValidateExistenceAsync<StudentSubmission>(submissionId);
            await ValidateExistenceAsync<Student>(dto.StudentId);
            await ValidateExistenceAsync<Attachment>(dto.AttachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<StudentSubmission>(submissionId);
            await ValidateForeignKeyAsync<Student>(dto.StudentId);
            await ValidateForeignKeyAsync<Attachment>(dto.AttachmentId);

            _mapper.Map(dto, studentSubmission);

            _context.Update(studentSubmission);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentSubmissionReadDto> CreateAssignmentStudentSubmissionAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, StudentSubmissionCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateExistenceAsync<Student>(dto.StudentId);
            await ValidateExistenceAsync<Attachment>(dto.AttachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);

            var studentSubmission = new StudentSubmission
            {
                StudentId = dto.StudentId,
                AssignmentId = assignmentId,
                AttachmentId = (int)dto.AttachmentId,
                Grade = dto.Grade
            };

            await _context.AddAsync(studentSubmission);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentSubmissionReadDto>(studentSubmission);
        }

        public async Task<StudentSubmissionReadDto> DeleteAssignmentStudentSubmissionAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId, int submissionId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            var studentSubmission = await ValidateExistenceAsync<StudentSubmission>(submissionId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<StudentSubmission>(submissionId);

            _context.Remove(studentSubmission);
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentSubmissionReadDto>(studentSubmission);
        }
        #endregion

        #region Assignments/Attachments
        public async Task<List<AssignmentAttachmentReadDto>> GetAssignmentAttachmentsAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var assignment = await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);

            await _context.Entry(assignment)
                .Collection(a => a.AssignmentAttachments)
                .Query()
                .Include(aa => aa.Attachment)
                .LoadAsync();

            return await _mapper.ProjectTo<AssignmentAttachmentReadDto>(
                assignment.AssignmentAttachments
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<AssignmentAttachmentReadDto> GetAssignmentAttachmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId, int attachmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateExistenceAsync<Attachment>(attachmentId);
            var assignmentAttachment = await ValidateExistenceAsync<AssignmentAttachment>(assignmentId, attachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<Attachment>(attachmentId);

            await _context.Entry(assignmentAttachment)
                .Reference(aa => aa.Attachment)
                .LoadAsync();

            return _mapper.Map<AssignmentAttachmentReadDto>(assignmentAttachment);
        }

        public async Task<AssignmentAttachmentReadDto> CreateAssignmentAttachmentAsync(string classCourseId,
            string classId, int classSemester, int classYear, int assignmentId, AssignmentAttachmentCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateExistenceAsync<Attachment>(dto.AttachmentId);
            await ValidateDuplicationAsync<AssignmentAttachment>(assignmentId, dto.AttachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var assignmentAttachment = new AssignmentAttachment
            {
                AssignmentId = assignmentId,
                AttachmentId = (int)dto.AttachmentId
            };

            await _context.AddAsync(assignmentAttachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssignmentAttachmentReadDto>(assignmentAttachment);
        }

        public async Task<AssignmentAttachmentReadDto> DeleteAssignmentAttachmentAsync(
            string classCourseId, string classId, int classSemester, int classYear, int assignmentId, int attachmentId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Assignment>(assignmentId);
            await ValidateExistenceAsync<Attachment>(attachmentId);
            var assignmentAttachment = await ValidateExistenceAsync<AssignmentAttachment>(assignmentId, attachmentId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Assignment>(assignmentId);
            await ValidateForeignKeyAsync<Attachment>(attachmentId);

            _context.Remove(assignmentAttachment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AssignmentAttachmentReadDto>(assignmentAttachment);
        }
        #endregion

        #region Groups
        public async Task<List<GroupReadDto>> GetGroupsAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.Groups)
                .Query()
                .LoadAsync();

            return await _mapper.ProjectTo<GroupReadDto>(
                inputClass.Groups
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<GroupReadDto> GetGroupAsync(
            string classCourseId, string classId, int classSemester, int classYear, int groupId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var group = await ValidateExistenceAsync<Group>(groupId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            return _mapper.Map<GroupReadDto>(group);
        }

        public async Task UpdateGroupAsync(
            string classCourseId, string classId, int classSemester,
            int classYear, int groupId, GroupCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var group = await ValidateExistenceAsync<Group>(groupId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _mapper.Map(dto, group);

            _context.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task<GroupReadDto> CreateGroupAsync(string classCourseId,
            string classId, int classSemester, int classYear, GroupCreateUpdateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            var group = new Group
            {
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
                Name = dto.Name,
                MaxSize = (int)dto.MaxSize
            };

            await _context.AddAsync(group);
            await _context.SaveChangesAsync();

            return _mapper.Map<GroupReadDto>(group);
        }

        public async Task<GroupReadDto> DeleteGroupAsync(
            string classCourseId, string classId, int classSemester, int classYear, int groupId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            var group = await ValidateExistenceAsync<Group>(groupId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _context.Remove(group);
            await _context.SaveChangesAsync();

            return _mapper.Map<GroupReadDto>(group);
        }
        #endregion

        #region Groups/Enrollments
        public async Task<List<EnrollmentReadDto>> GetGroupEnrollmentsAsync(
            string classCourseId, string classId, int classSemester, int classYear, int groupId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            var group = await ValidateExistenceAsync<Group>(groupId);

            await _context.Entry(group)
                .Collection(c => c.Enrollments)
                .Query()
                .Include(e => e.Student)
                .LoadAsync();

            return await _mapper.ProjectTo<EnrollmentReadDto>(
                group.Enrollments
                .AsQueryable()
            ).ToListAsyncFallback();
        }
        #endregion

        #region Lecturers
        public async Task<List<LecturerReadDto>> GetLecturersAsync(
            string classCourseId, string classId, int classSemester, int classYear)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.Lecturers)
                .Query()
                .Include(e => e.Teacher)
                .LoadAsync();

            return await _mapper.ProjectTo<LecturerReadDto>(
                inputClass.Lecturers
                .AsQueryable()
            ).ToListAsyncFallback();
        }

        public async Task<LecturerReadDto> CreateLecturerAsync(string classCourseId,
            string classId, int classSemester, int classYear, ClassLecturerCreateDto dto)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Teacher>(dto.TeacherId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<Teacher>(dto.TeacherId);
            await ValidateDuplicationAsync<Lecturer>(dto.TeacherId, classId, classSemester, classYear, classCourseId);

            var lecturer = new Lecturer
            {
                TeacherId = (int)dto.TeacherId,
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId,
            };

            await _context.AddAsync(lecturer);
            await _context.SaveChangesAsync();

            return _mapper.Map<LecturerReadDto>(lecturer);
        }

        public async Task<LecturerReadDto> DeleteLecturerAsync(
            string classCourseId, string classId, int classSemester, int classYear, int teacherId)
        {
            await ValidateExistenceAsync<T>(classCourseId);
            await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);
            await ValidateExistenceAsync<Teacher>(teacherId);
            var lecturer = await ValidateExistenceAsync<Lecturer>(
                teacherId, classId, classSemester, classYear, classCourseId);
            await ValidateForeignKeyAsync<T>(classCourseId);
            await ValidateForeignKeyAsync<Teacher>(teacherId);
            await ValidateForeignKeyAsync<Class>(classId, classSemester, classYear, classCourseId);

            _context.Remove(lecturer);
            await _context.SaveChangesAsync();

            return _mapper.Map<LecturerReadDto>(lecturer);
        }
        #endregion
    }
}
