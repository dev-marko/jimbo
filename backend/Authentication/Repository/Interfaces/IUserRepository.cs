using Authentication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Repository.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> FetchAllUsers();
        User FetchUserByEmail(string email);
        User FetchUserByUsername(string username);
        User VerifyUserCredentials(string username, string password);
        bool UserExists(string username, string email);
        User Insert(User entity);
        User Update(User entity);
        void Delete(User entity);
    }
}
