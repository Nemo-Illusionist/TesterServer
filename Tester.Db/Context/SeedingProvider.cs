using System;
using Microsoft.EntityFrameworkCore;
using Tester.Core.Common;
using Tester.Core.Constant;
using Tester.Db.Model.Client;

namespace Tester.Db.Context
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
                Salt = Guid.Parse("8A57AD97-45FE-4DB1-89C4-3973E0852177").ToString(),
                Password = "10-57-FD-75-B5-7A-95-53-F0-B7-92-16-4C-01-5F-8C", //admin
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