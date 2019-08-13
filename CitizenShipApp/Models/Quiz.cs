using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Models
{
    public class Quiz
    {
        private List<QuestionAnswers> _questionAnswers;
        public Quiz()
        {
            _questionAnswers = new List<QuestionAnswers>();
        }
        public List<QuestionAnswers> QuestionAnswers { get { return _questionAnswers; } }

        public void SetQuestionAnswers(List<QuestionAnswers> questionAnswers)
        {
            if (questionAnswers.Count < 1)
                throw new Exception("Quiz should have at least one question.");
            _questionAnswers = questionAnswers;
        }

        public int GetNumberOfCorrectAnswers()
        {
            var correct = _questionAnswers.Where(x => x.Result == true).Count();
            return correct;
        }

        public Decimal CalCulatePercentage()
        {
            Decimal percentage = 0;
            var correct = (decimal)_questionAnswers.Where(x => x.Result == true).Count();
            var r = ((correct / QuestionAnswers.Count) * 100);
            percentage = decimal.Round(((correct / QuestionAnswers.Count) * 100), 2);
            return percentage;
        }
    }
}
