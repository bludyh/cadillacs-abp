using System.Collections.Generic;

namespace Course.Common.Models
{
    public class Evaluation
    {
        public int Id { get; set; }
        public double Grade { get; set; }

        public List<StudentEvaluation> StudentEvaluations { get; set; }
        public List<GroupEvaluation> GroupEvaluations { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
