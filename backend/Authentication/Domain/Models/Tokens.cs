using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Domain.Models
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
