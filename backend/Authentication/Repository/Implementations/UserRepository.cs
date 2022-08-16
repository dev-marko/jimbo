using Authentication.Context;
using Authentication.Domain.Models;
using Authentication.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Authentication.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthenticationContext context;
        private readonly DbSet<User> entities;

        public UserRepository(AuthenticationContext context)
        {
            this.context = context;
            this.entities = context.Set<User>();
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<User> FetchAllUsers()
        {
            return entities.AsEnumerable();
        }

        public User FetchUserByEmail(string email)
        {
            return entities.SingleOrDefault(i => i.Email == email);
        }

        public User FetchUserByUsername(string username)
        {
            return entities.SingleOrDefault(i => i.Username == username);
        }

        public void Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public bool UserExists(string username, string email)
        {
            return (FetchUserByUsername(username) != null) || (FetchUserByEmail(email) != null);
        }

        public User VerifyUserCredentials(string username, string password)
        {
            User user = FetchUserByUsername(username);

            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
