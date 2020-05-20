using Infrastructure.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Dtos
{
    public class EmployeeUpdateDto
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Initials { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        // Picture

        public string SchoolId { get; set; }

        [CompositeKey(nameof(BuildingId))]
        public string RoomId { get; set; }
        public string BuildingId { get; set; }

    }
}
