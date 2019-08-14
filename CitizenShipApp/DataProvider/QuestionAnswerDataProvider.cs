using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenShipApp.Data;
using CitizenShipApp.Models;

namespace CitizenShipApp.DataProvider
{
    public class QuestionAnswerDataProvider : IQuestionAnswerDataProvider
    {
        private IQuestionAnswersRepository<QuestionAnswerXmlRepository> _repository;

        public QuestionAnswerDataProvider(IQuestionAnswersRepository<QuestionAnswerXmlRepository> repository)
        {
            _repository = repository;
        }
        public IResult<IEnumerable<QuestionAnswers>> GetNumberOfQuestionsByChapter(string chapterName, int numberOfQuestions)
        {
            return _repository.GetNumberOfQuestionsByChapter(chapterName, numberOfQuestions);
        }
    }
}
