using System;
using System.Collections.Generic;

namespace Identity.EventHandler.Models
{
    public partial class Program
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SchoolId { get; set; }

        public virtual School School { get; set; }
    }
}
