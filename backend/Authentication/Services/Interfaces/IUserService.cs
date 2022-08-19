using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using Authentication.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services.Interfaces
{
    public interface IUserService
    {
        List<User> FetchAllUsers();
        User FetchUserByEmail(string email);
        User FetchUserByUsername(string username);
        User Authenticate(UserLoginDTO userLoginDTO);
        UserViewModel Register(UserRegisterDTO userRegisterDTO);
        bool UserExists(UserRegisterDTO userRegisterDTO);
        User CreateUser(User entity);
        User UpdateUser(User entity);
        void DeleteUser(User entity);
    }
}
