using System.ComponentModel.DataAnnotations;

namespace Identity.Api.Dtos
{
    public class TeacherMentorCreateDto
    {

        [Required]
        public int? StudentId { get; set; }

        [Required]
        public string MentorType { get; set; }

    }
}
