using System;
using System.ComponentModel.DataAnnotations.Schema;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;

namespace Tester.Db.Model.Client
{
    [Table("user_role", Schema = DbConstant.Scheme.Client)]
    public class UserRole : ICreatedUtc, IDeletable
    {
        public Guid UserId { get; set; }

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