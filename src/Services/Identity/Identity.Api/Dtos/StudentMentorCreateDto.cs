using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Dtos
{
    public class StudentMentorCreateDto
    {

        [Required]
        public int? TeacherId { get; set; }

        [Required]
        public string MentorType { get; set; }

    }
}
