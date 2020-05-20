namespace Identity.Common.Models
{
    public class SchoolBuilding
    {

        public string SchoolId { get; set; }
        public School School { get; set; }

        public string BuildingId { get; set; }
        public Building Building { get; set; }

    }
}
