using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Models
{
    public class QuestionAnswers:BindableBase
    {
        public QuestionAnswers()
        {
            _answers = new List<Answer>();
        }

        private string _question;
        public string Question { get { return _question; } }

        private List<Answer> _answers;
        public List<Answer> Answers
        {
            get { return _answers; }
        }

        private bool _result;
        public bool Result
        {
            get { return _result; }
            set { SetProperty<bool>(ref this._result, value); }
        }

        private bool _isQuestionAnswered;

        public bool IsQuestionAnswered
        {
            get { return _isQuestionAnswered; }
            set { SetProperty<bool>(ref this._isQuestionAnswered, value); }
        }

        public void SetQuestion(string question)
        {
            _question = question;
        }

        public void SetAnswers(List<Answer> answers)
        {
            if (answers.Count > 4) throw new Exception("Answers count can not more than 4");
            if (answers.Count < 4) throw new Exception("Answers count can not be less than 4");
            if (answers.Where(x => x.IsCorrect == true).Count() < 1)
            {
                throw new Exception("At least one answer needs to be correct.");
            }
            if (answers.Where(x => x.IsCorrect == true).Count() > 1) throw new Exception("Only one answer can be correct.");
            _answers = answers;
        }
    }
}
