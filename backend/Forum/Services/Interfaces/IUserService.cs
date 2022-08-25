using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> FetchCurrentUser(string token);
        Task<User> FetchUserByUsername(string username);
        UserViewModel FetchUserViewModel(string username);
    }
}
