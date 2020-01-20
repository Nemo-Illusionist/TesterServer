using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;

namespace Tester.Db.Model.Client
{
    [Table("role", Schema = DbConstant.Scheme.Client)]
    public class Role : IEntity<Guid>, ICreatedUtc
    {
        [Key]
        [AutoIncrement]
        public Guid Id { get; set; }

        [Index(isUnique: true)]
        public string Name { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}