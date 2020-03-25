using NUnit.Framework;
using Tester.Auth.Contracts;
using Tester.Auth.Services;

namespace Tester.Tests.Unit
{
    public class PasswordProviderTest
    {
        private IPasswordProvider _passwordProvider;

        [SetUp]
        public void Init()
        {
            _passwordProvider = new PasswordProvider();
        }

        [TestCase("test")]
        public void VerifyPasswordTest(string password)
        {
            var hash = _passwordProvider.CreatePasswordHash(password);
            var isVerify = _passwordProvider.VerifyPasswordHash(password,hash);
            Assert.IsTrue(isVerify);
        }
    }
}