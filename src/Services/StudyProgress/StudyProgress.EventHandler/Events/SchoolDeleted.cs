using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProgress.EventHandler.Events
{
    public class SchoolDeleted : Event
    {
        public string Id { get; set; }
    }
}
