using System.Collections.Generic;

namespace Identity.Common.Models
{
    public class School
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public int? HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public List<Program> Programs { get; set; }
        public List<Employee> Employees { get; set; }
        public List<SchoolBuilding> SchoolBuildings { get; set; }

    }
}
