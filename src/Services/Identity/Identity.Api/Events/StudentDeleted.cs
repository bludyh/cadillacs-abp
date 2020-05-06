using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Events
{
    public class StudentDeleted : Event
    {
        public int Id { get; set; }
    }
}
