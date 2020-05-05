using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tester.Db.Model.App;

namespace Tester.Web.Admin.Services
{
    public interface IQuestionService
    {
        public IQueryable<Question> GetAll();
        public IQueryable<Question> GetById(Guid Id);
        public IQueryable<Question> GetAllByTopicId(Guid topicId);
        public Task Update(Question question);
        public Task Delete(Guid Id);
        public Task<Question> Create(Question question);
    }
}