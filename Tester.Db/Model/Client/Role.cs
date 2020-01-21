using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;

namespace Tester.Db.Model.Client
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("role", Schema = DbConstant.Scheme.Client)]
    public class Role : IEntity<Guid>, ICreatedUtc
    {
        [Key]
        [AutoIncrement]
        public Guid Id { get; set; }

        [Required]
        [Index(isUnique: true)]
        public string Name { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}