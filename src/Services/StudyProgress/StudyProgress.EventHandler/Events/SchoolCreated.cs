using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProgress.EventHandler.Events
{
    public class SchoolCreated : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
