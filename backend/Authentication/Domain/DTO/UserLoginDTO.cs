using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Domain.DTO
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
