using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common.Events
{
    public class StudentDeleted : Event
    {
        public int Id { get; set; }
    }
}
