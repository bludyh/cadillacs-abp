namespace Announcement.Api.Dtos
{
    public class ClassReadDto
    {
        public string Id { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
