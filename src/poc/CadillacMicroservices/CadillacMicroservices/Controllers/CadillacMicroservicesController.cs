using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CadillacMicroservices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadillacMicroservicesController : ControllerBase
    {

        private static readonly User[] Users = new User[]
        {
            new User("396244@student.fontys.nl","12345"),
            new User("123456@student.fontys.nl","12345")
        };

        private readonly ILogger<CadillacMicroservicesController> _logger;

        public CadillacMicroservicesController(ILogger<CadillacMicroservicesController> logger)
        {
            _logger = logger;
        }

       
        [HttpGet]
        public string Connect()
        {
            return "1";
        }

        [Route("signIn")]
        [HttpGet]
        public int SignIn([FromQuery] string email,[FromQuery] string password)
        {
            Console.WriteLine(email);
            if (ValidateEmail(email))
            {
                if (ValidatePassword(password))
                {
                    return 1;
                }
                return -1;
            }
            return 0;
        }

        private bool ValidateEmail(string email)
        {
            foreach(User user in Users)
            {
                if (user.UserEmail.Equals(email))
                    return true;
            }
            return false;
        }
        private bool ValidatePassword(string password)
        {
            foreach (User user in Users)
            {
                if (user.Password.Equals(password))
                    return true;
            }
            return false;
        }
    }
}
