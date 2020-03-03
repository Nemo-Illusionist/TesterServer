using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tester.Db.Model.Client;

namespace Auth.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}