using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;
using Tester.Core.Common;

namespace Tester.Db.Model.Client
{
    [Table("user_data", Schema = DbConstant.Scheme.Client)]
    public class UserData : IUpdatedUtc
    {
        [Key]
        public Guid UserId { get; set; }

        [Index]
        public string Name { get; set; }

        public Gender Gender { get; set; }
        public DateTime UpdatedUtc { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}