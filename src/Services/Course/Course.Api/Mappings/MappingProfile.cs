using AutoMapper;
using Course.Api.Dtos;
using Course.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Common.Models.Course, CourseReadDto>();
            CreateMap<CourseUpdateDto, Common.Models.Course>(MemberList.Source);
            CreateMap<CourseCreateDto, Common.Models.Course>(MemberList.Source);

            CreateMap<Class, ClassReadDto>();
            CreateMap<ClassCreateDto, Class>(MemberList.Source);

            CreateMap<StudyMaterial, StudyMaterialReadDto>();
            CreateMap<StudyMaterialCreateUpdateDto, StudyMaterial>(MemberList.Source);

            CreateMap<Enrollment, EnrollmentReadDto>();
            CreateMap<EnrollmentUpdateDto, Enrollment>(MemberList.Source);
            CreateMap<EnrollmentCreateDto, Enrollment>(MemberList.Source);

            CreateMap<Group, GroupReadDto>();
            CreateMap<GroupCreateUpdateDto, Group>(MemberList.Source);

            CreateMap<Student, StudentReadDto>();

            CreateMap<Assignment, AssignmentReadDto>();
            CreateMap<AssignmentCreateUpdateDto, Assignment>();

            CreateMap<Lecturer, LecturerReadDto>();
            CreateMap<ClassLecturerCreateDto, Lecturer>(MemberList.Source);
            CreateMap<TeacherLecturerCreateDto, Lecturer>(MemberList.Source);

            CreateMap<Teacher, TeacherReadDto>();

            CreateMap<Attachment, AttachmentReadDto>();
            CreateMap<AttachmentCreateUpdateDto, Attachment>(MemberList.Source);

            CreateMap<StudyMaterialAttachment, StudyMaterialAttachmentReadDto>();
            CreateMap<StudyMaterialAttachmentCreateDto, StudyMaterialAttachment>(MemberList.Source);

            CreateMap<AssignmentAttachment, AssignmentAttachmentReadDto>();
            CreateMap<AssignmentAttachmentCreateDto, AssignmentAttachment>(MemberList.Source);

            CreateMap<Submission, SubmissionReadDto>();
            CreateMap<StudentSubmission, StudentSubmissionReadDto>();
            CreateMap<StudentSubmissionCreateUpdateDto, StudentSubmission>(MemberList.Source);
        }
    }
}
