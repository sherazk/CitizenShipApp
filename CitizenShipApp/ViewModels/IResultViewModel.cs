using CitizenShipApp.Models;
using System.Collections.ObjectModel;

namespace CitizenShipApp.ViewModels
{
    public interface IResultViewModel
    {
        ObservableCollection<QuestionAnswers> Results { get; set; }
    }
}