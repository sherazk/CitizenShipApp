using CitizenShipApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CitizenShipApp.Data
{
    public interface IQuestionAnswersRepository<T> where T: class
    {
        IResult<IEnumerable<QuestionAnswers>> GetQuestionAnswersByChapter(int chapter);
        IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(int chapterNumber);
        IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(string chapterName);
        IResult<IEnumerable<string>> GetChapters();
        IResult<IEnumerable<QuestionAnswers>> GetNumberOfQuestionsByChapter(string chapterName, int numberOfQuestions);
    }
}