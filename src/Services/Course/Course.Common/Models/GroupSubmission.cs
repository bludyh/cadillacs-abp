namespace Course.Common.Models
{
    public class GroupSubmission : Submission
    {

        public int GroupId { get; set; }
        public Group Group { get; set; }

    }
}
