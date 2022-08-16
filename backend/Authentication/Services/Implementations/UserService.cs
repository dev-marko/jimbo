using Authentication.Domain.DTO;
using Authentication.Domain.Models;
using Authentication.Domain.ViewModels;
using Authentication.Repository.Interfaces;
using Authentication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Authentication.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Authenticate(UserLoginDTO userLoginDTO)
        {
            return userRepository.VerifyUserCredentials(userLoginDTO.Username, userLoginDTO.Password);
        }

        public void CreateUser(User entity)
        {
            string passwordHash = BC.HashPassword(entity.Password);
            entity.Password = passwordHash;
            userRepository.Insert(entity);
        }

        public void DeleteUser(User entity)
        {
            userRepository.Delete(entity);
        }

        public List<User> FetchAllUsers()
        {
            return userRepository.FetchAllUsers().ToList();
        }

        public User FetchUserByEmail(string email)
        {
            return userRepository.FetchUserByEmail(email);
        }

        public User FetchUserByUsername(string username)
        {
            return userRepository.FetchUserByUsername(username);
        }

        public UserViewModel Register(UserRegisterDTO userRegisterDTO)
        {
            User user = new User
            {
                Email = userRegisterDTO.Email,
                Username = userRegisterDTO.Username,
                Password = BC.HashPassword(userRegisterDTO.Password),
                Role = "StandardUser"  // Every user by default is a standard user
            };

            userRepository.Insert(user);

            return new UserViewModel 
            { 
                Email = user.Email, 
                Username = user.Username, 
                Role = user.Role
            };
        }

        public void UpdateUser(User entity)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(UserRegisterDTO userRegisterDTO)
        {
            return userRepository.UserExists(userRegisterDTO.Username, userRegisterDTO.Email);
        }
    }
}
