namespace StudyProgress.Api.Dtos
{
    public class ProgramReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalCredit { get; set; }
        public SchoolReadDto School { get; set; }
    }
}
