using Pitstop.Infrastructure.Messaging;

namespace Infrastructure.Common.Events
{
    public class EmployeeCreated : Event
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PicturePath { get; set; }
        public string AccountStatus { get; set; }

        public string SchoolId { get; set; }
        public string RoomId { get; set; }
        public string BuildingId { get; set; }

    }
}
