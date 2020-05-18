using System.ComponentModel.DataAnnotations;

namespace StudyProgress.Api.Dtos
{
    public class ProgramUpdateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int? TotalCredit { get; set; }
    }
}
