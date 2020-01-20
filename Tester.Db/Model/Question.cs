using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using REST.DataCore.Contract.Entity;
using REST.EfCore.Annotation;
using Tester.Core.Common;
using Tester.Db.Model.Client;
using Tester.Db.Model.Statistics;

namespace Tester.Db.Model
{
    [Table("question", Schema = DbConstant.Scheme.Default)]
    public class Question : IEntity<Guid>, ICreatedUtc, IDeletable
    {
        [Key]
        public Guid Id { get; set; }

        public Guid TopicId { get; set; }
        public Guid AuthorId { get; set; }

        [Index]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Hint { get; set; }

        [Index]
        public QuestionType Type { get; set; }

        [Column(TypeName = "jsonb")]
        public string Answer { get; set; }

        [Index]
        public DateTime CreatedUtc { get; set; }

        [Index]
        public DateTime? DeletedUtc { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        
        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public ICollection<UserAnswer> UserAnswer { get; set; }
    }
}