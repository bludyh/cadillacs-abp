﻿using Pitstop.Infrastructure.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Common.Events
{
    public class SchoolUpdated : Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
