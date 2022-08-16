using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services.Interfaces
{
    public interface IJWTManagerService
    {
        Tokens GenerateToken(User user);
    }
}
