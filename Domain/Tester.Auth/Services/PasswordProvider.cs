using System;
using System.Buffers.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Tester.Auth.Contracts;
using Tester.Auth.Models;

namespace Tester.Auth.Services
{
    public class PasswordProvider : IPasswordProvider
    {
        public PasswordHash CreatePasswordHash(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty", nameof(password));

            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return new PasswordHash(hash, salt);
        }

        public bool VerifyPasswordHash(string password, PasswordHash hash)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty", nameof(password));

            if (hash.Hash.Length != 64)
                throw new ArgumentException("Invalid length of password hash", nameof(hash));
            if (hash.Salt.Length != 128)
                throw new ArgumentException("Invalid length of password salt", nameof(hash));

            using (var hmac = new HMACSHA512(hash.Salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != hash.Hash[i]).Any())
                {
                    return false;
                }
            }

            return true;
        }
    }
}