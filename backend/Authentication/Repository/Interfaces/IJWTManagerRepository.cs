using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Repository.Interfaces
{
    public interface IJWTManagerRepository
    {
        User Authenticate(UserLoginDTO userLogin);
        Tokens GenerateToken(User user);
    }
}
