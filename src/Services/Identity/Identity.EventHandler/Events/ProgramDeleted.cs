using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.EventHandler.Events
{
    public class ProgramDeleted : Event
    {
        public string Id { get; set; }
    }
}
