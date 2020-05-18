﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Announcement.Api.Dtos;
using Announcement.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Announcement.Api.Controllers
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

        #region ClassAnnouncements
        // GET: api/Courses/5/Classes/e-s71/1/2020/Announcements
        [HttpGet("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/announcements")]
        public async Task<ActionResult<IEnumerable<ClassAnnouncementReadDto>>> GetClassAnnouncements(string classCourseId, string classId, int classSemester, int classYear)
        {
            return await _classService.GetClassAnnouncementsAsync(classCourseId, classId, classSemester, classYear);
        }

        // GET: api/Courses/prc1/Classes/e-s71/1/2020/Announcements/1
        [HttpGet("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/announcements/{announcementId}")]
        public async Task<ActionResult<ClassAnnouncementReadDto>> GetClassAnnouncement(string classCourseId, string classId, int classSemester, int classYear, int announcementId)
        {
            return await _classService.GetClassAnnouncementAsync(classCourseId, classId, classSemester, classYear, announcementId);
        }

        // POST: api/Courses/prc1/Classes/e-s71/1/2020/Announcements
        [HttpPost("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/announcements")]
        public async Task<ActionResult<ClassAnnouncementReadDto>> AddClassAnnouncement(string classCourseId, string classId, int classSemester, int classYear, [FromBody, Required] int announcementId)
        {
            var classAnnouncement = await _classService.CreateClassAnnouncementAsync(classCourseId, classId, classSemester, classYear, announcementId);

            return CreatedAtAction(nameof(GetClassAnnouncements), new { classCourseId, classId, classSemester, classYear, announcementId }, classAnnouncement);
        }

        // DELETE: api/Courses/prc1/Classes/e-s71/1/2020/Announcements/1
        [HttpDelete("{classCourseId}/[controller]/{classId}/{classSemester}/{classYear}/announcements/{announcementId}")]
        public async Task<ActionResult<ClassAnnouncementReadDto>> RemoveClassAnnouncement(string classCourseId, string classId, int classSemester, int classYear, int announcementId)
        {
            return await _classService.DeleteClassAnnouncementAsync(classCourseId, classId, classSemester, classYear, announcementId);
        }
        #endregion
    }
}