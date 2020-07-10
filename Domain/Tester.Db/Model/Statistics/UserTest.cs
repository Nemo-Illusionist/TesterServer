using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Data.Ef.Annotation;
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
        
        public bool IsOver { get; set; }
        
        public bool IsChecked { get; set; }
        
        public int RightAnswers { get; set; }
        
        public int WrongAnswers { get; set; }

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