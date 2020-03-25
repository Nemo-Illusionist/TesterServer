using System;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Common;
using Tester.Core.Constant;
using Tester.Db.Model.Client;

namespace Tester.Db.Provider
{
    internal static class SeedingProvider
    {
        public static ModelBuilder BuildSeeding(ModelBuilder builder)
        {
            RoleSeeding(builder);
            AdminSeeding(builder);
            return builder;
        }

        private static void AdminSeeding(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("60396F59-DCD2-4045-9029-793DF7CEE7EA"),
                Login = "admin",
                Salt = new byte[]
                {
                    64, 132, 145, 88, 10, 194, 112, 102, 76, 212, 254, 162, 125, 138, 223, 21, 4, 81, 183, 120, 84, 126,
                    249, 91, 62, 130, 18, 98, 178, 248, 222, 142, 22, 154, 45, 110, 115, 84, 65, 117, 211, 184, 143, 18,
                    163, 142, 61, 51, 186, 90, 171, 19, 13, 43, 237, 203, 240, 200, 26, 220, 197, 203, 107, 79, 195, 28,
                    216, 213, 98, 204, 10, 100, 217, 22, 188, 30, 11, 252, 76, 60, 200, 196, 215, 43, 249, 16, 30, 132,
                    0, 251, 117, 32, 179, 220, 242, 96, 191, 183, 113, 36, 81, 51, 136, 119, 252, 31, 230, 119, 167,
                    130, 190, 7, 18, 209, 30, 75, 216, 129, 55, 27, 98, 27, 108, 163, 176, 33, 19, 118
                },
                Password = new byte[]
                {
                    252, 29, 38, 134, 205, 60, 211, 7, 6, 166, 238, 109, 252, 219, 114, 230, 31, 160, 3, 66, 95, 52, 43,
                    254, 67, 79, 170, 23, 245, 25, 139, 232, 83, 176, 200, 234, 163, 191, 147, 143, 72, 111, 76, 207,
                    76, 172, 183, 154, 145, 160, 161, 114, 55, 204, 25, 110, 216, 186, 201, 126, 123, 70, 31, 253
                }, //admin
                CreatedUtc = DateTime.MinValue,
                UpdatedUtc = DateTime.MinValue,
                DeletedUtc = null,
                SecurityTimestamp = Guid.Parse("B73DF749-8157-4F3F-880E-A083E0D90B4C")
            });

            builder.Entity<UserData>().HasData(new UserData
            {
                UserId = Guid.Parse("60396F59-DCD2-4045-9029-793DF7CEE7EA"),
                Gender = Gender.Undefined,
                Name = "admin",
                LastName = null,
                UpdatedUtc = DateTime.MinValue
            });

            builder.Entity<UserRole>().HasData(new UserRole
            {
                RoleId = Guid.Parse("F7B718A4-535F-4E91-8B1E-8C65012C8960"),
                UserId = Guid.Parse("60396F59-DCD2-4045-9029-793DF7CEE7EA"),
                CreatedUtc = DateTime.MinValue,
                DeletedUtc = null
            });
        }

        private static void RoleSeeding(ModelBuilder builder)
        {
            var roles = new[]
            {
                new Role
                {
                    Id = Guid.Parse("F7B718A4-535F-4E91-8B1E-8C65012C8960"),
                    Name = RoleNameConstant.Admin
                },
                new Role
                {
                    Id = Guid.Parse("427912C2-1327-463D-A0FC-5E864528AEB0"),
                    Name = RoleNameConstant.Moderator
                },
                new Role
                {
                    Id = Guid.Parse("6CB9DC41-D6B5-4CD9-B0B8-A085F5742449"),
                    Name = RoleNameConstant.Lecturer
                },
                new Role
                {
                    Id = Guid.Parse("3638240A-0A54-42A6-81FB-3393D7336684"),
                    Name = RoleNameConstant.Student
                }
            };
            foreach (var role in roles)
            {
                role.CreatedUtc = DateTime.MinValue;
                builder.Entity<Role>().HasData(role);
            }
        }
    }
}