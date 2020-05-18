using Announcement.Api.Dtos;
using Announcement.Common.Data;
using Announcement.Common.Models;
using AutoMapper;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement.Api.Services
{
    public interface IClassService
    {
        public Task<List<ClassAnnouncementReadDto>> GetClassAnnouncementsAsync(string classCourseId, string classId, int classSemester, int classYear);
        public Task<ClassAnnouncementReadDto> GetClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId);
        public Task<ClassAnnouncementReadDto> CreateClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId);
        public Task<ClassAnnouncementReadDto> DeleteClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId);

    }
    public class ClassService : ServiceBase, IClassService
    {
        private readonly IMapper _mapper;

        public ClassService(AnnouncementContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        #region ClassAnnouncements
        public async Task<List<ClassAnnouncementReadDto>> GetClassAnnouncementsAsync(string classCourseId, string classId, int classSemester, int classYear)
        {
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await _context.Entry(inputClass)
                .Collection(c => c.ClassAnnouncements)
                .Query()
                .Include(ca => ca.Announcement)
                .ThenInclude(a => a.Employee)
                .LoadAsync();

            return await _mapper.ProjectTo<ClassAnnouncementReadDto>(inputClass.ClassAnnouncements.AsQueryable()).ToListAsyncFallback();
        }

        public async Task<ClassAnnouncementReadDto> GetClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId)
        {
            var classAnnouncement = await ValidateExistenceAsync<ClassAnnouncement>(announcementId, classId, classSemester, classYear, classCourseId);

            await _context.Entry(classAnnouncement)
                .Reference(ca => ca.Announcement)
                .Query()
                .Include(a => a.Employee)
                .LoadAsync();

            return _mapper.Map<ClassAnnouncementReadDto>(classAnnouncement);
        }

        public async Task<ClassAnnouncementReadDto> CreateClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId)
        {
            var inputClass = await ValidateExistenceAsync<Class>(classId, classSemester, classYear, classCourseId);

            await ValidateExistenceAsync<Common.Models.Announcement>(announcementId);

            await ValidateDuplicationAsync<ClassAnnouncement>(announcementId, classId, classSemester, classYear, classCourseId);

            var classAnnouncement = new ClassAnnouncement
            {
                AnnouncementId = announcementId,
                ClassId = classId,
                ClassSemester = classSemester,
                ClassYear = classYear,
                ClassCourseId = classCourseId
            };

            await _context.AddAsync(classAnnouncement);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassAnnouncementReadDto>(classAnnouncement);
        }

        public async Task<ClassAnnouncementReadDto> DeleteClassAnnouncementAsync(string classCourseId, string classId, int classSemester, int classYear, int announcementId)
        {
            var classAnnouncement = await ValidateExistenceAsync<ClassAnnouncement>(announcementId, classId, classSemester, classYear, classCourseId);

            await _context.Entry(classAnnouncement)
                .Reference(ca => ca.Announcement)
                .Query()
                .Include(a => a.Employee)
                .LoadAsync();

            _context.Remove(classAnnouncement);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClassAnnouncementReadDto>(classAnnouncement);
        }
        #endregion
    }
}
