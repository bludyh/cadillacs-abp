using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadillacMicroservices
{
    public class User
    {
        public string UserEmail
        {
            get;set;
        }
        public string Password
        {
            get;set;
        }
        public User(string userEmail, string password)
        {
            this.UserEmail = userEmail;
            this.Password = password;
        }
    }
}
