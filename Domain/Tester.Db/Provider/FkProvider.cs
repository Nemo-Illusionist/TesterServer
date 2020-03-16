using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;
using Tester.Db.Model.Statistics;

namespace Tester.Db.Provider
{
    internal static class FkProvider
    {
        public static ModelBuilder BuildFk([NotNull] ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Entity<Topic>()
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId);

            builder.Entity<Topic>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Topics)
                .HasForeignKey(x => x.AuthorId);

            builder.Entity<Question>()
                .HasOne(x => x.Topic)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.TopicId);

            builder.Entity<Question>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Questions)
                .HasForeignKey(x => x.AuthorId);

            builder.Entity<Test>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.AuthorId);

            builder.Entity<TestTopic>()
                .HasOne(x => x.Test)
                .WithMany(x => x.TestTopics)
                .HasForeignKey(x => x.TestId);

            builder.Entity<TestTopic>()
                .HasOne(x => x.Topic)
                .WithMany(x => x.TestTopics)
                .HasForeignKey(x => x.TopicId);

            builder.Entity<UserData>()
                .HasOne(x => x.User)
                .WithOne(x => x.UserData)
                .HasForeignKey<UserData>(x => x.UserId);

            builder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<UserTest>()
                .HasOne(x => x.Test)
                .WithMany(x => x.UserTests)
                .HasForeignKey(x => x.TestId);

            builder.Entity<UserTest>()
                .HasOne(x => x.Examiner)
                .WithMany(x => x.Observers)
                .HasForeignKey(x => x.ExaminerId);

            builder.Entity<UserTest>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTests)
                .HasForeignKey(x => x.UserId);
            
            builder.Entity<UserAnswer>()
                .HasOne(x => x.Question)
                .WithMany(x => x.UserAnswer)
                .HasForeignKey(x => x.QuestionId);
            
            builder.Entity<UserAnswer>()
                .HasOne(x => x.UserTest)
                .WithMany(x => x.UserAnswer)
                .HasForeignKey(x => x.UserTestId);
            
            return builder;
        }
    }
}