using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Data.Ef.Annotation;

namespace Tester.Db.Model.Client
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("user_role", Schema = DbConstant.Scheme.Client)]
    public class UserRole : ICreatedUtc, IDeletable
    {
        [MultiKey]
        public Guid UserId { get; set; }

        [MultiKey]
        public Guid RoleId { get; set; }

        public DateTime CreatedUtc { get; set; }

        [Index]
        public DateTime? DeletedUtc { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
    }
}