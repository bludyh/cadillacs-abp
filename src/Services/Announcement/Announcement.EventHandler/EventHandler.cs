using Announcement.Common.Data;
using Announcement.Common.Models;
using AutoMapper;
using Infrastructure.Common.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Announcement.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly AnnouncementContext _announcementContext;
        private readonly IMessageHandler _messageHandler;
        private readonly IMapper _mapper;

        public EventHandler(AnnouncementContext announcementContext, IMessageHandler messageHandler, IMapper mapper)
        {
            _announcementContext = announcementContext;
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
                case "EmployeeCreated":
                    await HandleAsync(messageObject.ToObject<EmployeeCreated>());
                    break;
                case "EmployeeDeleted":
                    await HandleAsync(messageObject.ToObject<EmployeeDeleted>());
                    break;
                case "EmployeeUpdated":
                    await HandleAsync(messageObject.ToObject<EmployeeUpdated>());
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
            }

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeCreated e)
        {
            var employee = _mapper.Map<Employee>(e);

            await _announcementContext.AddAsync(employee);
            await _announcementContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeDeleted e)
        {
            var employee = await _announcementContext.FindAsync<Employee>(e.Id);

            _announcementContext.Remove(employee);
            await _announcementContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(EmployeeUpdated e)
        {
            var employee = await _announcementContext.FindAsync<Employee>(e.Id);

            _mapper.Map(e, employee);

            _announcementContext.Update(employee);
            await _announcementContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassCreated e)
        {
            var _class = _mapper.Map<Class>(e);

            await _announcementContext.AddAsync(_class);
            await _announcementContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassDeleted e)
        {
            var _class = await _announcementContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _announcementContext.Remove(_class);
            await _announcementContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassUpdated e)
        {
            var _class = await _announcementContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _mapper.Map(e, _class);

            _announcementContext.Update(_class);
            await _announcementContext.SaveChangesAsync();

            return true;
        }
    }
}
