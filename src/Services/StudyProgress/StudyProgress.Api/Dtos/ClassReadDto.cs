using System;

namespace StudyProgress.Api.Dtos
{
    public class ClassReadDto
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CourseReadDto Course { get; set; }
    }
}
