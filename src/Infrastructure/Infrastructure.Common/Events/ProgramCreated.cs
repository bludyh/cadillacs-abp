﻿using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common.Events
{
    public class ProgramCreated : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalCredit { get; set; }

        public string SchoolId { get; set; }
    }
}
