using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Data.Ef.Annotation;
using Tester.Db.Model.App;

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

        /// <summary>
        /// User response. The structure depends on the type of question
        /// </summary>
        [Required]
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