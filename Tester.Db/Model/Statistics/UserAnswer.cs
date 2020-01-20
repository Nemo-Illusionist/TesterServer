using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;

namespace Tester.Db.Model.Statistics
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("user_answer", Schema = DbConstant.Scheme.Report)]
    public class UserAnswer : IEntity<Guid>, ICreatedUtc
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserTestId { get; set; }

        public Guid QuestionId { get; set; }

        [Column(TypeName = "jsonb")]
        public string Value { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }

        [ForeignKey(nameof(UserTestId))]
        public UserTest UserTest { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
    }
}