using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.Events;
using CitizenShipApp.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CitizenShipApp.ViewModels
{
    public class QuestionAnswersViewModel:BindableBase, IQuestionAnswersViewModel
    {
        private List<QuestionAnswers> _questionAnswersList;
        private int _currentIndex;
        private IEventAggregator _eventAggregator;
        //IQuestionAnswersRepository<QuestionAnswerXmlRepository> _repository;
        private IQuestionAnswerDataProvider _questionAnswerDataProvider;

        public QuestionAnswersViewModel(IEventAggregator eventAggregator,
            IQuestionAnswerDataProvider questionAnswerDataProvider
            //,IQuestionAnswersRepository<QuestionAnswerXmlRepository> repository
            )
        {
            //_repository = repository;
            _questionAnswerDataProvider = questionAnswerDataProvider;
            _currentIndex = 0;
            NextQuestionCommand = new DelegateCommand(OnNextQuestionExecuted,CanMoveToNextQuestionExecuted);
            AnswerSelectedCommand = new DelegateCommand(OnAnswerSelectedExecuted);
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<QuizStartupEvent>().Subscribe(InitializeQuestionAnswersForQuiz);
        }

        private QuestionAnswers questionAnswers;
        public QuestionAnswers QuestionAnswers
        {
            get { return questionAnswers; }
            set { SetProperty<QuestionAnswers>(ref this.questionAnswers, value); }
        }

        private int _TotalQuestions;

        public int TotalQuestions
        {
            get { return _TotalQuestions; }
            set { SetProperty<int>(ref this._TotalQuestions, value); }
        }

        private Answer _SelectedAnswer;
        public Answer SelectedAnswer
        {
            get { return _SelectedAnswer; }
            set
            {
                SetProperty<Answer>(ref this._SelectedAnswer, value);
                //if (value != null)
                //{
                //    QuestionAnswers.Result = value.IsCorrect;
                //    QuestionAnswers.IsQuestionAnswered = true;
                //    ((DelegateCommand)NextQuestionCommand).RaiseCanExecuteChanged();
                //    QuestionAnswers.Answers.Where(x => x.AnswerValue == value.AnswerValue)
                //        .FirstOrDefault(x => x.IsSelectedAnswer = true);
                //    if (_currentIndex == _questionAnswersList.Count - 1)
                //    {
                //        _eventAggregator.GetEvent<QuizCompletedEvent>().Publish(_questionAnswersList);
                //    }
                //}
            }
        }

        private int _questioNumber;
        public int QuestionNumber
        {
            get { return _currentIndex+1; }
            set { SetProperty<int>(ref this._questioNumber, value); }
        }

        public ICommand NextQuestionCommand { get; }

        public ICommand AnswerSelectedCommand { get; }

        public ICommand PreviousQuestionCommand { get; }

        private void InitializeQuestionAnswersForQuiz(QuizStartup quizStartup)
        {
            IResult<IEnumerable<QuestionAnswers>> result = _questionAnswerDataProvider.GetNumberOfQuestionsByChapter(quizStartup.ChapterName, quizStartup.NumberOfQuesitons);
            if (result !=null && result.IsSucess)
            {
                _currentIndex = 0;
                SelectedAnswer = null;
                QuestionAnswers = null;
                _questionAnswersList = new List<QuestionAnswers>(result.Data);
                QuestionAnswers = _questionAnswersList[0];
                TotalQuestions = _questionAnswersList.Count;
            }
        }

        private bool CanMoveToNextQuestionExecuted()
        {
            if(_questionAnswersList == null)
            {
                throw new NullReferenceException("Question Answer list is not initialized.");
            }
            
            if (_currentIndex == _questionAnswersList.Count - 1)
            {
                return false;
            }

            if (!QuestionAnswers.IsQuestionAnswered)
            {
                return false;
            }
            return true;
        }
        private void OnNextQuestionExecuted()
        {
            if(_currentIndex< _questionAnswersList.Count-1)
             {
                 _currentIndex++;
                QuestionNumber = _currentIndex + 1;
                 QuestionAnswers = _questionAnswersList[_currentIndex];
                ((DelegateCommand)NextQuestionCommand).RaiseCanExecuteChanged();
            }
        }

        private void OnAnswerSelectedExecuted()
        {
            if (SelectedAnswer != null)
            {
                QuestionAnswers.Result = SelectedAnswer.IsCorrect;
                QuestionAnswers.IsQuestionAnswered = true;
                ((DelegateCommand)NextQuestionCommand).RaiseCanExecuteChanged();
                QuestionAnswers.Answers.Where(x => x.AnswerValue == SelectedAnswer.AnswerValue)
                    .FirstOrDefault(x => x.IsSelectedAnswer = true);
                if (_currentIndex == _questionAnswersList.Count - 1)
                {
                    _eventAggregator.GetEvent<QuizCompletedEvent>().Publish(_questionAnswersList);
                }
            }
        }

        //private bool CanMoveToPreviousQuestion()
        //{
        //    if (_currentIndex == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //private void OnPreviousQuestion()
        //{
        //    if(_currentIndex>0)
        //    {
        //        _currentIndex--;
        //        QuestionAnswers = _questionAnswersList[_currentIndex];
        //        ((DelegateCommand)NextQuestionCommand).RaiseCanExecuteChanged();
        //        ((DelegateCommand)PreviousQuestionCommand).RaiseCanExecuteChanged();
        //    }
        //}



    }
}

