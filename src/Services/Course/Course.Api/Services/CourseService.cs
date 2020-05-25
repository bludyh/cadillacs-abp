using AutoMapper;
using Course.Api.Dtos;
using Course.Common.Data;
using Infrastructure.Common;
using Infrastructure.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Services
{
    public interface ICourseService
    {
        public Task<List<CourseReadDto>> GetAllAsync();
        public Task<CourseReadDto> GetAsync(string courseId);
        public Task UpdateAsync(string courseId, CourseUpdateDto dto);
        public Task<CourseReadDto> CreateAsync(CourseCreateDto dto);
        public Task<CourseReadDto> DeleteAsync(string courseId);
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
    }
}
