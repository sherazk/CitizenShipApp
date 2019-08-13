using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Models
{
    public class Answer:BindableBase
    {
        private bool _isCorrect;
        public bool IsCorrect
        {
            get { return _isCorrect; }
            set { SetProperty<bool>(ref this._isCorrect, value); }
        }

        private bool _isSelectedAnswer;

        public bool IsSelectedAnswer
        {
            get { return _isSelectedAnswer; }
            set { SetProperty<bool>(ref this._isSelectedAnswer, value); }
        }


        public string AnswerValue { get; set; }
    }
}
