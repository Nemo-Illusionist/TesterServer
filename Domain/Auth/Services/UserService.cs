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
            if (!VerifyPasswordHash(password, 
                Encoding.UTF8.GetBytes(user.Password), 
                Encoding.UTF8.GetBytes(user.Salt)))
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
                throw new AppException("Password is required");

            if (_context.Users.Any(x => x.Login == user.Login))
                throw new AppException("Username \"" + user.Login + "\" is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

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
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.Login) && userParam.Login != user.Login)
            {
                // throw error if the new username is already taken
                if (_context.Users.Any(x => x.Login == userParam.Login))
                    throw new AppException("Username " + userParam.Login + " is already taken");

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
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

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
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", 
                    nameof(password));

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        private static bool VerifyPasswordHash(string password, IReadOnlyList<byte> storedHash, byte[] storedSalt)
        {
            if (password == null) 
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty or whitespace only string.", 
                    nameof(password));
            if (storedHash.Count != 64) 
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", 
                    "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", 
                    "passwordHash");
            // var bytes = Encoding.UTF8.GetBytes(storedSalt + password);
            // var hash = BitConverter.ToString(SHA256.Create().ComputeHash(bytes));
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }
            return true;
        }
        // [UsedImplicitly]
        // public class PasswordGenerator
        // {
        //     public string MakeHash(string salt, string password)
        //     {
        //         if (string.IsNullOrWhiteSpace(salt))
        //             throw new ArgumentException("Value cannot be null or whitespace.", nameof(salt));
        //         if (string.IsNullOrWhiteSpace(password))
        //             throw new ArgumentException("Value cannot be null or whitespace.", nameof(password));
        //
        //         var bytes = Encoding.UTF8.GetBytes(salt + password);
        //         var hash = BitConverter.ToString(SHA256.Create().ComputeHash(bytes));
        //
        //         return hash;
        //     }
        // }
    }
}