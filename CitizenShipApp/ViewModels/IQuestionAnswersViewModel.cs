using CitizenShipApp.Models;
using System.Windows.Input;

namespace CitizenShipApp.ViewModels
{
    public interface IQuestionAnswersViewModel
    {
        QuestionAnswers QuestionAnswers { get; set; }
        Answer SelectedAnswer { get; set; }
        int QuestionNumber { get; set; }
        ICommand NextQuestionCommand { get; }
        ICommand AnswerSelectedCommand { get; }

    }
}