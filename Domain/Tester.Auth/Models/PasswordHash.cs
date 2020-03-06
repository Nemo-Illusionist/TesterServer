using System;
using JetBrains.Annotations;

namespace Tester.Auth.Models
{
    public struct PasswordHash
    {
        public string Hash { get; }
        public string Salt { get; }

        public PasswordHash([NotNull] string hash, [NotNull] string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(salt));
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(hash));
            Hash = hash;
            Salt = salt;
        }
    }
}