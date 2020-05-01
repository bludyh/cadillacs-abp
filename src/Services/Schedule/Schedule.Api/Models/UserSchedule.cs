using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Api.Models
{
    public class UserSchedule
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime{ get; set; }

        public string Description{ get; set; }

        public User User { get; set; }
    }
}
