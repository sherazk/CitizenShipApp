using Autofac;
using CitizenShipApp.Data;
using CitizenShipApp.Events;
using CitizenShipApp.Startup;
using CitizenShipApp.ViewModels;
using Moq;
using NUnit.Framework;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Test.ViewModel
{
    [TestFixture]
    public class QuestionAnswersViewModelTest
    {
        IQuestionAnswersViewModel viewModel;
        [SetUp]
        public void Setup()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();
            viewModel = container.Resolve<IQuestionAnswersViewModel>();
        }
        [Test]
        public void ShouldPublishQuizCompletedEvent()
        {
            var _questionAnswersList = DummyQuestionAnswers.LoadQuestionAnswers();
            var eventMock = new Mock<QuizCompletedEvent>();
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(ea => ea.GetEvent<QuizCompletedEvent>())
                .Returns(eventMock.Object);
            var list = DummyQuestionAnswers.LoadQuestionAnswers();
            viewModel.QuestionAnswers = list[0];
            viewModel.SelectedAnswer = list[0].Answers[1];
            viewModel.SelectedAnswer = list[1].Answers[2];
            viewModel.SelectedAnswer = list[2].Answers[3];
            eventMock.Verify(e => e.Publish(list), Times.Once);
        }
    }
}
