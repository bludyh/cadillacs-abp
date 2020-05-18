using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Dtos
{
    public class StudentUpdateDto
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

        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Required]
        public string ProgramId { get; set; }

    }
}
