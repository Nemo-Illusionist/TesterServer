using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;
using Tester.Db.Model.App;
using Tester.Db.Model.Statistics;

namespace Tester.Db.Model.Client
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("user", Schema = DbConstant.Scheme.Client)]
    public class User : IEntity<Guid>, ICreatedUtc, IUpdatedUtc, IDeletable
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Index(isUnique: true)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public Guid SecurityTimestamp { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }

        public DateTime UpdatedUtc { get; set; }

        [Index]
        public DateTime? DeletedUtc { get; set; }
        
        [CanBeNull] public string FirstName { get; set; }
        
        [CanBeNull] public string LastName { get; set; }
        
        public UserData UserData { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<UserTest> Observers { get; set; }
        public ICollection<UserTest> UserTests { get; set; }
    }
}