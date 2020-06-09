using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Radilovsoft.Rest.Data.Core.Contract;
using Radilovsoft.Rest.Data.Core.Contract.Provider;
using Tester.Db.Model.App;
using Tester.Db.Model.Client;

namespace Tester.Web.Admin.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IDataProvider _dataProvider;
        public QuestionService(
            [NotNull] IDataProvider dataProvider,
            [NotNull] IAsyncHelpers asyncHelpers)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public IQueryable<Question> GetAll()
        {
            return _dataProvider.GetQueryable<Question>();
        }

        public IQueryable<Question> GetById(Guid Id)
        {
            return _dataProvider.GetQueryable<Question>().Where(x => x.Id == Id);
        }

        public IQueryable<Question> GetAllByTopicId(Guid topicId)
        {
            var query = _dataProvider.GetQueryable<Question>()
                .Where(x => x.Topic.Id.Equals(topicId));
            return query;
        }

        public async Task Update(Question question)
        {
            await _dataProvider.UpdateAsync(question).ConfigureAwait(false);
        }

        public async Task Delete(Guid Id)
        {
            await _dataProvider.DeleteByIdAsync<Question, Guid>(Id).ConfigureAwait(false);
        }

        public async Task<Question> Create(Question question)
        {
            var user = await _dataProvider.GetQueryable<User>()
                .Where(x => question.AuthorId == x.Id).FirstOrDefaultAsync();
            var topic = await _dataProvider.GetQueryable<Topic>()
                .Where(x => question.TopicId == x.Id).FirstOrDefaultAsync();
            var res = await _dataProvider.InsertAsync(question).ConfigureAwait(false);
            return res;
        }
    }
}