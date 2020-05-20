namespace Identity.Common.Models
{
    public class EmployeeProgram
    {

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string ProgramId { get; set; }
        public Program Program { get; set; }

    }
}
