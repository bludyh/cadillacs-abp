using System.ComponentModel.DataAnnotations;

namespace StudyProgress.Api.Dtos
{
    public class StudentEnrollmentCreateDto
    {
        [Required]
        public string ClassId { get; set; }

        [Required]
        public int? ClassSemester { get; set; }

        [Required]
        public int? ClassYear { get; set; }

        [Required]
        public string ClassCourseId { get; set; }

    }
}
