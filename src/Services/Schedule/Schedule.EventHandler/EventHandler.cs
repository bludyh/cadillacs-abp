using AutoMapper;
using Infrastructure.Common.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using Schedule.Common.Data;
using Schedule.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Schedule.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly ScheduleContext _scheduleContext;
        private readonly IMessageHandler _messageHandler;
        private readonly IMapper _mapper;

        public  EventHandler(ScheduleContext scheduleContext, IMessageHandler messageHandler, IMapper mapper)
        {
            _scheduleContext = scheduleContext;
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
                case "ClassCreated":
                    await HandleAsync(messageObject.ToObject<ClassCreated>());
                    break;
                case "ClassDeleted":
                    await HandleAsync(messageObject.ToObject<ClassDeleted>());
                    break;
                case "ClassUpdated":
                    await HandleAsync(messageObject.ToObject<ClassUpdated>());
                    break;
                case "RoomCreated":
                    await HandleAsync(messageObject.ToObject<RoomCreated>());
                    break;
                case "RoomDeleted":
                    await HandleAsync(messageObject.ToObject<RoomDeleted>());
                    break;
            }

            return true;
        }

        private async Task<bool> HandleAsync(ClassCreated e)
        {
            var _class = _mapper.Map<Class>(e);

            await _scheduleContext.AddAsync(_class);
            await _scheduleContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassDeleted e)
        {
            var _class = await _scheduleContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _scheduleContext.Remove(_class);
            await _scheduleContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ClassUpdated e)
        {
            var _class = await _scheduleContext.FindAsync<Class>(e.Id, e.Semester, e.Year, e.CourseId);

            _mapper.Map(e, _class);

            _scheduleContext.Update(_class);
            await _scheduleContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(RoomCreated e)
        {
            var room = _mapper.Map<Room>(e);

            await _scheduleContext.AddAsync(room);
            await _scheduleContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(RoomDeleted e)
        {
            var room = await _scheduleContext.FindAsync<Room>(e.Id, e.BuildingId);

            _scheduleContext.Remove(room);
            await _scheduleContext.SaveChangesAsync();

            return true;
        }
    }
}
