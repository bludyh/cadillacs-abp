namespace Identity.Api.Dtos
{
    public class StudentMentorReadDto
    {
        public string MentorType { get; set; }
        public EmployeeReadDto Teacher { get; set; }
    }
}
