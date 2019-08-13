using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.Events;
using CitizenShipApp.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CitizenShipApp.ViewModels
{
    public class MainViewModel:BindableBase
    {
        private IQuizStartupViewModel _quizStartupViewModel;
        private IQuestionAnswersViewModel _questionAnswersViewModel;
        private IResultViewModel _resultViewModel;
        private IEventAggregator _eventAggregator;
        public MainViewModel(IQuestionAnswersViewModel questionAnswersViewModel,
            IResultViewModel resultViewModel,
            IQuizStartupViewModel quizStartupViewModel,
            IEventAggregator eventAggregator)
        {
            _quizStartupViewModel = quizStartupViewModel;
            _questionAnswersViewModel = questionAnswersViewModel;
            _resultViewModel = resultViewModel;
            QuestionAnswers = new ObservableCollection<QuestionAnswers>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<QuizCompletedEvent>().Subscribe(OnQuizCompletionSwitchViewToResultView);
            StartCommand = new DelegateCommand(OnQuizStartExecuted); 
        }

        private QuizStartup _quizStartup;
        public QuizStartup QuizStartup
        {
            get { return _quizStartup; }
            set { SetProperty(ref _quizStartup, value); }
        }

        private string _chapterName;
        public string ChapterName
        {
            get { return _chapterName; }
            set { SetProperty<string>(ref this._chapterName, value); }
        }

        private ObservableCollection<QuestionAnswers> _questionAnswers;
        public ObservableCollection<QuestionAnswers> QuestionAnswers
        {
            get { return _questionAnswers; }
            set { SetProperty<ObservableCollection<QuestionAnswers>>(ref this._questionAnswers, value); }
        }

        private object _currentViewModel;
        public object CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty<object>(ref this._currentViewModel, value); }
        }

        public ICommand StartCommand { get; }

        public void LoadView()
        {
            CurrentViewModel = _quizStartupViewModel;
        }

        private void OnQuizStartExecuted()
        {
            _quizStartup = _quizStartupViewModel.LoadQuizStartupSettings();
            ChapterName = _quizStartup.ChapterName;
            _eventAggregator.GetEvent<QuizStartupEvent>().Publish(_quizStartup);
            if (CurrentViewModel == _resultViewModel)
            {
                CurrentViewModel = _quizStartupViewModel;
            }
            else
            {
                CurrentViewModel = _questionAnswersViewModel;
            }
        }

        private void OnQuizCompletionSwitchViewToResultView(IEnumerable<QuestionAnswers> obj)
        {
            CurrentViewModel = _resultViewModel;
        }
        public void LoadsQuestionAnswers()
        {
            QuestionAnswers = new ObservableCollection<QuestionAnswers>(DummyQuestionAnswers.LoadQuestionAnswers());
        }

        
    }
}
