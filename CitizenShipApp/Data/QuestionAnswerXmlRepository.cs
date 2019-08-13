using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CitizenShipApp.Models;
using CitizenShipApp.Properties;

namespace CitizenShipApp.Data
{
    public class QuestionAnswerXmlRepository :IQuestionAnswersRepository<QuestionAnswerXmlRepository>
    {
        private string _fileData;
        public QuestionAnswerXmlRepository()
        {
            _fileData = Resources.QuizData;
        }

        public IResult<IEnumerable<string>> GetChapters()
        {
            IResult<IEnumerable<string>> result = new Result<IEnumerable<string>>();
            if (_fileData == null)
                throw new ArgumentNullException();
            try
            {
                TextReader textReader = new StringReader(_fileData);
                var doc = XDocument.Load(textReader);
                var Chapters = doc.Descendants("Chapters").Elements("Chapter")
                    .Select(x => x.Attribute("ChapterName").Value).ToList();
                result.Data = Chapters;
                result.IsSucess = true;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionAnswersByChapter(int chapter)
        {
            throw new NotImplementedException();
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(int chapterNumber)
        {
            IResult<IEnumerable<QuestionAnswers>> result = new Result<IEnumerable<QuestionAnswers>>();
            if (_fileData == null)
                throw new ArgumentNullException();
            try
            {
                TextReader textReader = new StringReader(_fileData);
                var doc = XDocument.Load(textReader);
                var questionAnswersList = doc.Descendants("Chapter")
                    .Where(x => x.Attribute("ChapterId").Value == chapterNumber.ToString())
                    .Elements("Quiz").OrderBy(x => Guid.NewGuid()).ToList();
                var list = new List<QuestionAnswers>();
                foreach (var questionAnswers in questionAnswersList)
                {
                    QuestionAnswers questionAnswer = new QuestionAnswers();
                    questionAnswer.SetQuestion(questionAnswers.Attribute("Question").Value);
                    List<Answer> answers = new List<Answer>();
                    var ansList = questionAnswers.Elements("Answer")
                        .Select(a => new Answer()
                        {
                            AnswerValue = a.Attribute("Answer").Value,
                            IsCorrect = int.Parse(a.Attribute("IsCorrectAnswer").Value) == 1 ? true : false
                        }).ToList();
                    questionAnswer.SetAnswers(ansList.OrderBy(x => Guid.NewGuid()).Take(ansList.Count).ToList());

                    list.Add(questionAnswer);
                }
                result.Data = list.ToList();
                result.IsSucess = true;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetQuestionsByChapter(string chapterName)
        {
            IResult<IEnumerable<QuestionAnswers>> result = new Result<IEnumerable<QuestionAnswers>>();
            if (_fileData == null)
                throw new ArgumentNullException();
            try
            {
                TextReader textReader = new StringReader(_fileData);
                var doc = XDocument.Load(textReader);
                var questionAnswersList = doc.Descendants("Chapter")
                    .Where(x => x.Attribute("ChapterName").Value == chapterName)
                    .Elements("Quiz").OrderBy(x => Guid.NewGuid()).ToList();
                var list = new List<QuestionAnswers>();
                foreach (var questionAnswers in questionAnswersList)
                {
                    QuestionAnswers questionAnswer = new QuestionAnswers();
                    questionAnswer.SetQuestion(questionAnswers.Attribute("Question").Value);
                    List<Answer> answers = new List<Answer>();
                    var ansList = questionAnswers.Elements("Answer")
                        .Select(a => new Answer()
                        {
                            AnswerValue = a.Attribute("Answer").Value,
                            IsCorrect = int.Parse(a.Attribute("IsCorrectAnswer").Value) == 1 ? true : false
                        }).ToList();
                    questionAnswer.SetAnswers(ansList.OrderBy(x => Guid.NewGuid()).Take(ansList.Count).ToList());

                    list.Add(questionAnswer);
                }
                result.Data = list.ToList();
                result.IsSucess = true;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public IResult<IEnumerable<QuestionAnswers>> GetNumberOfQuestionsByChapter(string chapterName,int numberOfQuestions)
        {
            IResult<IEnumerable<QuestionAnswers>> result = new Result<IEnumerable<QuestionAnswers>>();
            if (_fileData == null)
                throw new ArgumentNullException();
            try
            {
                TextReader textReader = new StringReader(_fileData);
                var doc = XDocument.Load(textReader);
                var questionAnswersList = doc.Descendants("Chapter")
                    .Where(x => x.Attribute("ChapterName").Value == chapterName)
                    .Elements("Quiz").OrderBy(x => Guid.NewGuid()).ToList().Take(numberOfQuestions);
                var list = new List<QuestionAnswers>();
                foreach (var questionAnswers in questionAnswersList)
                {
                    QuestionAnswers questionAnswer = new QuestionAnswers();
                    questionAnswer.SetQuestion(questionAnswers.Attribute("Question").Value);
                    List<Answer> answers = new List<Answer>();
                    var ansList = questionAnswers.Elements("Answer")
                        .Select(a => new Answer()
                        {
                            AnswerValue = a.Attribute("Answer").Value,
                            IsCorrect = int.Parse(a.Attribute("IsCorrectAnswer").Value) == 1 ? true : false
                        }).ToList();
                    questionAnswer.SetAnswers(ansList.OrderBy(x => Guid.NewGuid()).Take(ansList.Count).ToList());

                    list.Add(questionAnswer);
                }
                result.Data = list.ToList();
                result.IsSucess = true;
            }
            catch (Exception ex)
            {
                result.IsSucess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
