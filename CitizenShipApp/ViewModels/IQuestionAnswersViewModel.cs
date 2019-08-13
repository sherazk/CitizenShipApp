using CitizenShipApp.Models;

namespace CitizenShipApp.ViewModels
{
    public interface IQuestionAnswersViewModel
    {
        QuestionAnswers QuestionAnswers { get; set; }
        Answer SelectedAnswer { get; set; }
    }
}