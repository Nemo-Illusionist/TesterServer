using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Radilovsoft.Rest.Data.Core.Contract.Entity;
using Radilovsoft.Rest.Data.Ef.Annotation;

namespace Tester.Db.Model.App
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CA2227")]
    [Table("test_topic", Schema = DbConstant.Scheme.Default)]
    public class TestTopic : ICreatedUtc, IDeletable
    {
        [MultiKey]
        public Guid TestId { get; set; }

        [MultiKey]
        public Guid TopicId { get; set; }

        public DateTime CreatedUtc { get; set; }

        [Index]
        public DateTime? DeletedUtc { get; set; }


        [ForeignKey(nameof(TestId))]
        public Test Test { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
    }
}