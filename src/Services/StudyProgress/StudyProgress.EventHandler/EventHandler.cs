using AutoMapper;
using Infrastructure.Common.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using StudyProgress.EventHandler.Data;
using StudyProgress.EventHandler.Models;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
