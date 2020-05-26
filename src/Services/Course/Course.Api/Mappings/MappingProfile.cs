﻿using AutoMapper;
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

            CreateMap<StudyMaterialAttachment, StudyMaterialAttachmentReadDto>();

            CreateMap<Enrollment, EnrollmentReadDto>();
            CreateMap<EnrollmentCreateDto, Enrollment>(MemberList.Source);
            CreateMap<Group, GroupReadDto>();
            CreateMap<Student, StudentReadDto>();

            CreateMap<Assignment, AssignmentReadDto>();
            CreateMap<AssignmentCreateUpdateDto, Assignment>();
        }
    }
}
