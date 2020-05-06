using AutoMapper;
using Identity.EventHandler.Data;
using Identity.EventHandler.Events;
using Identity.EventHandler.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {

        private readonly IdentityContext _identityContext;
        private readonly IMessageHandler _messageHandler;
        private readonly IMapper _mapper;

        public EventHandler(IdentityContext identityContext, IMessageHandler messageHandler, IMapper mapper)
        {
            _identityContext = identityContext;
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

            switch(messageType)
            {
                case "ProgramCreated":
                    await HandleAsync(messageObject.ToObject<ProgramCreated>());
                    break;
                case "ProgramDeleted":
                    await HandleAsync(messageObject.ToObject<ProgramDeleted>());
                    break;
                case "ProgramUpdated":
                    await HandleAsync(messageObject.ToObject<ProgramUpdated>());
                    break;
            }

            return true;
        }

        private async Task<bool> HandleAsync(ProgramCreated e)
        {
            var program = _mapper.Map<Models.Program>(e);

            await _identityContext.AddAsync(program);
            await _identityContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ProgramDeleted e)
        {
            var program = await _identityContext.FindAsync<Models.Program>(e.Id);

            _identityContext.Remove(program);
            await _identityContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> HandleAsync(ProgramUpdated e)
        {
            var program = await _identityContext.FindAsync<Models.Program>(e.Id);

            _mapper.Map(e, program);

            _identityContext.Update(program);
            await _identityContext.SaveChangesAsync();

            return true;
        }

    }
}
