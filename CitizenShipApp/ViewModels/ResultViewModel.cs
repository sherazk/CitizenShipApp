using CitizenShipApp.Events;
using CitizenShipApp.Models;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.ViewModels
{
    public class ResultViewModel:BindableBase,IResultViewModel
    {
        private IEventAggregator _eventAggregator;

        public ResultViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<QuizCompletedEvent>().Subscribe(OnQuizCompletionCreateResult);
        }

        private double _percentage;

        public double Percentage
        {
            get { return _percentage; }
            set { SetProperty<double>(ref this._percentage, value); }
        }

        private ObservableCollection<QuestionAnswers> _results;

        public ObservableCollection<QuestionAnswers> Results
        {
            get { return _results; }
            set { SetProperty<ObservableCollection<QuestionAnswers>>(ref this._results, value); }
        }
            
        private void OnQuizCompletionCreateResult(IEnumerable<QuestionAnswers> quizList)
        {
            Results = new ObservableCollection<QuestionAnswers>(quizList);
            double totalCorrectAnswers = quizList.Where(x => x.Result == true).Count();
            Percentage = (totalCorrectAnswers / quizList.Count()) * 100;
            Percentage = double.Parse(string.Format("{0:0.00}", Percentage));
        }
    }
}
