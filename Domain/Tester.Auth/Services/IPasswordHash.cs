namespace Auth.Services
{
    public struct HashStruct
    {
        public byte[] passwordHash;
        public byte[] passwordSalt;
    }
    public interface IPasswordHash
    {
        HashStruct CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
    }
}