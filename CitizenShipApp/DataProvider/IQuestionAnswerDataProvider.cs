using CitizenShipApp.Models;
using System.Collections.Generic;

namespace CitizenShipApp.DataProvider
{
    public interface IQuestionAnswerDataProvider
    {
        IResult<IEnumerable<QuestionAnswers>> GetNumberOfQuestionsByChapter(string chapterName, int numberOfQuestions);
    }
}