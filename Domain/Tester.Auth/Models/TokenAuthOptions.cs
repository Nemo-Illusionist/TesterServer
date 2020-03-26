using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.IdentityModel.Tokens;

namespace Tester.Auth.Models
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class TokenAuthOptions
    {
        public string Path { get; } = "/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(30);

        public SigningCredentials SigningCredentials { get; set; }

        public Func<Task<string>> NonceGenerator { get; }
            = () => Task.FromResult(Guid.NewGuid().ToString());
    }
}