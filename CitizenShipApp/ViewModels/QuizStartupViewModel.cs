using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.Models;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace CitizenShipApp.ViewModels
{
    public class QuizStartupViewModel : BindableBase, IQuizStartupViewModel
    {
        //private IEventAggregator _eventAggregator;
        private IQuizStartupDataProvider _quizStartupDataProvider;
        private QuizStartup _quizStartup;

        public QuizStartupViewModel(IQuizStartupDataProvider quizStartupDataProvider)
        {
            //_eventAggregator = eventAggregator;
            _quizStartupDataProvider = quizStartupDataProvider;
            _quizStartup = new QuizStartup();

            LoadStartupValues();
        }

        private ObservableCollection<string> _chapters;

        public ObservableCollection<string> Chapters
        {
            get { return _chapters; }
            set { SetProperty<ObservableCollection<string>>(ref this._chapters, value); }
        }

        private string _selectedChapter;

        public string SelectedChapter
        {
            get { return _selectedChapter; }
            set
            {
                SetProperty<string>(ref this._selectedChapter, value);
                PublishStartupQuiz();
            }
        }

        private ObservableCollection<int> _numberOfQuestions;

        public ObservableCollection<int> NumberOfQuestions
        {
            get { return _numberOfQuestions; }
            set
            {
                SetProperty<ObservableCollection<int>>(ref this._numberOfQuestions, value);
                PublishStartupQuiz();
            }
        }

        private int _selectedNumber;

        public int SelectedNumber
        {
            get { return _selectedNumber; }
            set { SetProperty<int>(ref this._selectedNumber, value); }
        }

        private void LoadStartupValues()
        {
            NumberOfQuestions = new ObservableCollection<int>() { 5, 10, 15, 20, 25, 30, 40, 50, 60, 80 };
            SelectedNumber = NumberOfQuestions[0];
            LoadChapters();
            //PublishStartupQuiz();
        }

        private void LoadChapters()
        {
            var result = _quizStartupDataProvider.GetChapters();
            if (result.IsSucess)
            {
                Chapters = new ObservableCollection<string>(result.Data);
                SelectedChapter = Chapters[0];
            }
        }

        public QuizStartup LoadQuizStartupSettings()
        {
            return _quizStartup;
        }

        private void PublishStartupQuiz()
        {
            if (!string.IsNullOrEmpty(SelectedChapter))
            {
                _quizStartup.ChapterName = SelectedChapter;
                _quizStartup.NumberOfQuesitons = SelectedNumber;
            }
        }
    }
}
