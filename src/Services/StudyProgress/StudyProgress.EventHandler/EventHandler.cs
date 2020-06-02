using AutoMapper;
using Infrastructure.Common.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using StudyProgress.Common.Data;
using StudyProgress.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StudyProgress.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly StudyProgressContext _studyProgressContext;
        private readonly IMessageHandler _messageHandler;
        private readonly IMapper _mapper;

        public EventHandler(StudyProgressContext studyProgressContext, IMessageHandler messageHandler, IMapper mapper)
        {
            _studyProgressContext = studyProgressContext;
            _messageHandler = messageHandler;
            _mapper = mapper;
        }

        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            JObject messageObject = MessageSerializer.Deserialize(message);

            switch (messageType)
            {
                case "SchoolCreated":
                    await HandleAsync(messageObject.ToObject<SchoolCreated>());
                    break;
                case "SchoolDeleted":
                    await HandleAsync(messageObject.ToObject<SchoolDeleted>());
                    break;
                case "SchoolUpdated":
                    await HandleAsync(messageObject.ToObject<SchoolUpdated>());
                    break;
                case "StudentCreated":
                    await HandleAsync(messageObject.ToObject<StudentCreated>());
                    break;
                case "StudentDeleted":
                    await HandleAsync(messageObject.ToObject<StudentDeleted>());
                    break;
                case "StudentUpdated":
                    await HandleAsync(messageObject.ToObject<StudentUpdated>());
                    break;
                case "CourseCreated":
                    await HandleAsync(messageObject.ToObject<CourseCreated>());
                    break;
                case "CourseDeleted":
                    await HandleAsync(messageObject.ToObject<CourseDeleted>());
                    break;
                case "CourseUpdated":
                    await HandleAsync(messageObject.ToObject<CourseUpdated>());
                    break;
                case "ClassCreated":
                    await HandleAsync(messageObject.ToObject<ClassCreated>());
                    break;
                case "ClassDeleted":
                    await HandleAsync(messageObject.ToObject<ClassDeleted>());
                    break;
                case "ClassUpdated":
                    await HandleAsync(messageObject.ToObject<ClassUpdated>());
                    break;
                case "EnrollmentCreated":
                    await HandleAsync(messageObject.ToObject<EnrollmentCreated>());
                    break;
                case "EnrollmentDeleted":
                    await HandleAsync(messageObject.ToObject<EnrollmentDeleted>());
                    break;
                case "EnrollmentUpdated":
                    await HandleAsync(messageObject.ToObject<EnrollmentUpdated>());
                    break;
            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true;
        }

        private async Task<bool> HandleAsync(SchoolCreated e)
        {
            var school = _mapper.Map<School>(e);

            await _studyProgressContext.AddAsync(school);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(SchoolDeleted e)
        {
            var school = await _studyProgressContext.FindAsync<School>(e.Id);

            _studyProgressContext.Remove(school);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(SchoolUpdated e)
        {
            var school = await _studyProgressContext.FindAsync<School>(e.Id);

            _mapper.Map(e, school);

            _studyProgressContext.Update(school);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(StudentCreated e)
        {
            var student = _mapper.Map<Student>(e);

            await _studyProgressContext.AddAsync(student);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(StudentDeleted e)
        {
            var student = await _studyProgressContext.FindAsync<Student>(e.Id);

            _studyProgressContext.Remove(student);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(StudentUpdated e)
        {
            var student = await _studyProgressContext.FindAsync<Student>(e.Id);

            _mapper.Map(e, student);

            _studyProgressContext.Update(student);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(CourseCreated e)
        {
            var course = _mapper.Map<Course>(e);

            await _studyProgressContext.AddAsync(course);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(CourseDeleted e)
        {
            var course = await _studyProgressContext.FindAsync<Course>(e.Id);

            _studyProgressContext.Remove(course);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(CourseUpdated e)
        {
            var course = await _studyProgressContext.FindAsync<Course>(e.Id);

            _mapper.Map(e, course);

            _studyProgressContext.Update(course);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassCreated e)
        {
            var _class = _mapper.Map<Class>(e);

            await _studyProgressContext.AddAsync(_class);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassDeleted e)
        {
            var _class = await _studyProgressContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _studyProgressContext.Remove(_class);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassUpdated e)
        {
            var _class = await _studyProgressContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _mapper.Map(e, _class);

            _studyProgressContext.Update(_class);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EnrollmentCreated e)
        {
            var enrollment = _mapper.Map<Enrollment>(e);

            await _studyProgressContext.AddAsync(enrollment);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EnrollmentDeleted e)
        {
            var enrollment = await _studyProgressContext.FindAsync<Enrollment>(e.ClassId, e.ClassSemester, e.ClassYear, e.ClassCourseId, e.StudentId);

            _studyProgressContext.Remove(enrollment);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EnrollmentUpdated e)
        {
            var enrollment = await _studyProgressContext.FindAsync<Enrollment>(e.ClassId, e.ClassSemester, e.ClassYear, e.ClassCourseId, e.StudentId);

            _mapper.Map(e, enrollment);

            _studyProgressContext.Update(enrollment);
            await _studyProgressContext.SaveChangesAsync();

            return true;
        }
    }
}
