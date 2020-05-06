using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProgress.EventHandler.Events
{
    public class StudentDeleted : Event
    {
        public int Id { get; set; }
    }
}
