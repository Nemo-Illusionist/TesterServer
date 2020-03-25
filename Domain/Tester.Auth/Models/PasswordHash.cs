using System;
using JetBrains.Annotations;

namespace Tester.Auth.Models
{
    public struct PasswordHash
    {
        public byte[] Hash { get; }
        public byte[] Salt { get; }

        public PasswordHash([NotNull] byte[] hash, [NotNull] byte[] salt)
        {
            if (hash == null || hash.Length != 64)
                throw new ArgumentException("Invalid hash", nameof(hash));
            if (salt == null || salt.Length != 128)
                throw new ArgumentException("Invalid salt", nameof(hash));
            Hash = hash;
            Salt = salt;
        }
    }
}