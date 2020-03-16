using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.Db.Model.Statistics
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("user_test", Schema = DbConstant.Scheme.Report)]
    public class UserTest : IEntity<Guid>, ICreatedUtc
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TestId { get; set; }
        public Guid UserId { get; set; }

        public Guid? ExaminerId { get; set; }

        public TimeSpan? Time { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }


        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(ExaminerId))]
        public User Examiner { get; set; }

        public ICollection<UserAnswer> UserAnswer { get; set; }
    }
}