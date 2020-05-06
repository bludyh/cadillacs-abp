using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.EventHandler.Events
{
    public class ProgramUpdated : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SchoolId { get; set; }
    }
}
