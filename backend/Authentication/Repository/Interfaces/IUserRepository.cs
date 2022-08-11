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
        void Insert(User entity);
        void Update(User entity);
        void Delete(User entity);
    }
}
