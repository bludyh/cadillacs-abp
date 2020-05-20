using System.ComponentModel.DataAnnotations;

namespace StudyProgress.Api.Dtos
{
    public class ProgramCreateDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int? TotalCredit { get; set; }

        [Required]
        public string SchoolId { get; set; }
    }
}
