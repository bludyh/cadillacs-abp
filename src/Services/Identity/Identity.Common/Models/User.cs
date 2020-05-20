﻿using Microsoft.AspNetCore.Identity;

namespace Identity.Common.Models
{
    public abstract class User : IdentityUser<int>
    {

        // Id is inherited from IdentityUser
        // Username is used as PCN

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }

        // Email and PhoneNumber are inherited from IdentityUser

        public string PicturePath { get; set; }

        public AccountStatus AccountStatus { get; set; }

        // PasswordHash is inherited from IdentityUser

    }
}
