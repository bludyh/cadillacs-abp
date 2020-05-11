using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common.Events
{
    public class ProgramDeleted : Event
    {
        public string Id { get; set; }
    }
}
