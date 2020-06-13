using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Auth.Services;
using Tester.Core.Common;
using Tester.Db.Model.Client;

namespace Tester.TestDataGenerator.DataGenerator
{
    public static class UserGenerator
    {
        public static async Task<IEnumerable<User>> Gen([NotNull] IDataProvider dataProvider)
        {
            if (dataProvider == null) throw new ArgumentNullException(nameof(dataProvider));

            var passwordProvider = new PasswordProvider();
            var fakerUser = new Faker<User>()
                .RuleFor(x => x.Login, (f, u) => f.Internet.Email())
                .RuleFor(x => x.SecurityTimestamp, f => Guid.NewGuid())
                .FinishWith((f, u) =>
                    {
                        var passwordHash = passwordProvider.CreatePasswordHash(f.Internet.Password());
                        u.Password = passwordHash.Hash;
                        u.Salt = passwordHash.Salt;
                    }
                );
            var fakerUserData = new Faker<UserData>()
                .RuleFor(x => x.Name, (f, u) => f.Name.FirstName())
                .RuleFor(x => x.LastName, (f, u) => f.Name.LastName())
                .RuleFor(x => x.Gender, (f, u) => f.PickRandom<Gender>());

            var roles = await dataProvider.GetQueryable<Role>().ToArrayAsync().ConfigureAwait(false);

            var users = new List<User>();
            foreach (var role in roles)
            {
                for (int i = 0; i < 100; i++)
                {
                    var user = fakerUser.Generate();
                    await dataProvider.InsertAsync(user).ConfigureAwait(false);
                    var userData = fakerUserData.Generate();
                    await dataProvider.InsertAsync(userData).ConfigureAwait(false);
                    var userRole = new UserRole {UserId = user.Id, RoleId = role.Id, Role = role, User = user};
                    await dataProvider.InsertAsync(userRole).ConfigureAwait(false);
                    user.UserData = userData;
                    user.UserRoles = new List<UserRole> {userRole};
                    users.Add(user);
                }
            }

            return users;
        }
    }
}