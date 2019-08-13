using System.Collections.ObjectModel;
using CitizenShipApp.Models;

namespace CitizenShipApp.ViewModels
{
    public interface IQuizStartupViewModel
    {
        ObservableCollection<string> Chapters { get; set; }
        string SelectedChapter { get; set; }
        int SelectedNumber { get; set; }
        QuizStartup LoadQuizStartupSettings();
    }
}