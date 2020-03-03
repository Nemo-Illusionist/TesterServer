using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Services
{
    public class PasswordHash: IPasswordHash
    {
        public HashStruct CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) 
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty", nameof(password));

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return new HashStruct
            {
                passwordHash=passwordHash, 
                passwordSalt=passwordSalt
            };
        }
        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var stHash = Encoding.UTF8.GetBytes(storedHash);
            var stSalt = Encoding.UTF8.GetBytes(storedSalt);
            if (password == null) 
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) 
                throw new ArgumentException("Value cannot be empty", nameof(password));
            if (stHash.Length != 64) 
                throw new ArgumentException("Invalid length of password hash", "passwordHash");
            if (storedSalt.Length != 128) 
                throw new ArgumentException("Invalid length of password salt", "passwordHash");
            // var bytes = Encoding.UTF8.GetBytes(storedSalt + password);
            // var hash = BitConverter.ToString(SHA256.Create().ComputeHash(bytes));
            using (var hmac = new HMACSHA512(stSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != storedHash[i]).Any())
                {
                    return false;
                }
            }
            return true;
        }
    }
}