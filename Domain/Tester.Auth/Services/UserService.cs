using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Auth.Helpers;
using JetBrains.Annotations;
using Tester.Db.Model.Client;

namespace Auth.Services
{
    public class UserService: IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        
        public User Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Login == login);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!PasswordHash.VerifyPasswordHash(password, user.Password, user.Salt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll()
        {    
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User Create(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required");

            if (_context.Users.Any(x => x.Login == user.Login))
                throw new ArgumentException("Username \"" + user.Login + "\" is already taken");

            PasswordHash.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.Password = Encoding.UTF8.GetString(passwordHash);
            user.Salt = Encoding.UTF8.GetString(passwordSalt);

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new ArgumentException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Login) && userParam.Login != user.Login)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Login == userParam.Login))
                    throw new ArgumentException("Username " + userParam.Login + " is already taken");

                user.Login = userParam.Login;
            }
            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
                user.FirstName = userParam.FirstName;

            if (!string.IsNullOrWhiteSpace(userParam.LastName))
                user.LastName = userParam.LastName;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                PasswordHash.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.Password = Encoding.UTF8.GetString(passwordHash);
                user.Salt = Encoding.UTF8.GetString(passwordSalt);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}