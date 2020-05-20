using System.Collections.Generic;

namespace Announcement.Common.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }

        public List<Announcement> Announcements { get; set; }
    }
}
