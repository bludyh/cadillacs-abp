using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }

        public string Description { get; set; }
    }
}
