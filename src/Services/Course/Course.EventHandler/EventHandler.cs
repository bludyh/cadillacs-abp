using AutoMapper;
using Course.Common.Data;
using Course.Common.Models;
using Infrastructure.Common.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Course.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly CourseContext _courseContext;
        private readonly IMessageHandler _messageHandler;
        private readonly IMapper _mapper;

        public EventHandler(CourseContext courseContext, IMessageHandler messageHandler, IMapper mapper)
        {
            _courseContext = courseContext;
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
                case "StudentCreated":
                    await HandleAsync(messageObject.ToObject<StudentCreated>());
                    break;
                case "StudentDeleted":
                    await HandleAsync(messageObject.ToObject<StudentDeleted>());
                    break;
                case "StudentUpdated":
                    await HandleAsync(messageObject.ToObject<StudentUpdated>());
                    break;
                case "EmployeeCreated":
                    await HandleAsync(messageObject.ToObject<EmployeeCreated>());
                    break;
                case "EmployeeDeleted":
                    await HandleAsync(messageObject.ToObject<EmployeeDeleted>());
                    break;
                case "EmployeeUpdated":
                    await HandleAsync(messageObject.ToObject<EmployeeUpdated>());
                    break;
            }

            return true;
        }

        private async Task<bool> HandleAsync(StudentCreated e)
        {
            var student = _mapper.Map<Student>(e);

            await _courseContext.AddAsync(student);
            await _courseContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(StudentDeleted e)
        {
            var student = await _courseContext.FindAsync<Student>(e.Id);

            _courseContext.Remove(student);
            await _courseContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(StudentUpdated e)
        {
            var student = await _courseContext.FindAsync<Student>(e.Id);

            _mapper.Map(e, student);

            _courseContext.Update(student);
            await _courseContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeCreated e)
        {
            var teacher = _mapper.Map<Teacher>(e);

            await _courseContext.AddAsync(teacher);
            await _courseContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeDeleted e)
        {
            var teacher = await _courseContext.FindAsync<Teacher>(e.Id);

            _courseContext.Remove(teacher);
            await _courseContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeUpdated e)
        {
            var teacher = await _courseContext.FindAsync<Teacher>(e.Id);

            _mapper.Map(e, teacher);

            _courseContext.Update(teacher);
            await _courseContext.SaveChangesAsync();

            return true;
        }

    }
}
