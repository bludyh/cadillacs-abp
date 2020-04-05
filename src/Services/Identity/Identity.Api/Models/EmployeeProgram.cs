using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Api.Models {
    public class EmployeeProgram {

        public int EmployeeId { get; set; } 
        public Employee Employee { get; set; }

        public string ProgramId { get; set; }
        public Program Program { get; set; }

    }
}
