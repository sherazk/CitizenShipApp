using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenShipApp.Data;
using CitizenShipApp.Models;

namespace CitizenShipApp.DataProvider
{
    public class QuizStartupDataProvider : IQuizStartupDataProvider
    {
        private IQuestionAnswersRepository<QuestionAnswerXmlRepository> _repository;

        public QuizStartupDataProvider(IQuestionAnswersRepository<QuestionAnswerXmlRepository> repository)
        {
            _repository = repository;
        }
        public IResult<IEnumerable<string>> GetChapters()
        {
            return _repository.GetChapters();
        }
    }
}
