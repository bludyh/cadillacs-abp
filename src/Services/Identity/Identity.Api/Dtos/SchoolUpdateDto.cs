using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Dtos
{
    public class SchoolUpdateDto
    {

        [Required]
        public string Name { get; set; }

        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
