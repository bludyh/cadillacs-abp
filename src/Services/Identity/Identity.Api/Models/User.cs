using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Models {
    public abstract class User : IdentityUser<int> {

        // Id is inherited from IdentityUser
        // Username is used as PCN

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }

        // Email and PhoneNumber are inherited from IdentityUser

        public string PicturePath { get; set; }

        public AccountStatus AccountStatus { get; set; }

        // PasswordHash is inherited from IdentityUser

        public string SchoolId { get; set; }
        public School School { get; set; }

    }
}
