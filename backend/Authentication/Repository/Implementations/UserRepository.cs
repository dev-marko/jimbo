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

        public User Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var user = entities.Add(entity).Entity;
            context.SaveChanges();
            return user;
        }

        public User Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var user = entities.Update(entity).Entity;
            context.SaveChanges();
            return user;
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
