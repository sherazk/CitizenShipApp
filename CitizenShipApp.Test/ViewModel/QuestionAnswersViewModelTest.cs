using Autofac;
using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.Events;
using CitizenShipApp.Models;
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
        private Mock<IEventAggregator> _eventAggregator;
        private Mock<IQuestionAnswerDataProvider> _questionAnswerDataProviderMock;
        private QuizStartupEvent _quizStartupEvent;

        [SetUp]
        public void Setup()
        {
            _eventAggregator = new Mock<IEventAggregator>();
            _quizStartupEvent = new QuizStartupEvent();
            _eventAggregator.Setup(ea => ea.GetEvent<QuizStartupEvent>())
                .Returns(_quizStartupEvent);

            _questionAnswerDataProviderMock = new Mock<IQuestionAnswerDataProvider>();
            _questionAnswerDataProviderMock
                .Setup(d => d.GetNumberOfQuestionsByChapter(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(new Result<IEnumerable<QuestionAnswers>>()
                {
                    IsSucess = true,
                    Data = DummyQuestionAnswers.LoadQuestionAnswers()
                });

            viewModel = new QuestionAnswersViewModel(_eventAggregator.Object,
                _questionAnswerDataProviderMock.Object);

        }

        [Test]
        public void ShouldInitializeQuestionAnswersWhenQuizStarts()
        {
            var quizStartup = new QuizStartup() { ChapterName = "test", NumberOfQuesitons = 5 };
            _quizStartupEvent.Publish(quizStartup);

            Assert.AreEqual("What is name of the capital of Ontrario?", viewModel.QuestionAnswers.Question);
        }

        [Test]
        public void ShouldMoveToNextQuestionWhenOnAnswerSelectedExecuted()
        {
            var quizStartup = new QuizStartup() { ChapterName = "test", NumberOfQuesitons = 5 };
            _quizStartupEvent.Publish(quizStartup);
            Assert.AreEqual("What is name of the capital of Ontrario?", viewModel.QuestionAnswers.Question);
            Assert.AreEqual(1, viewModel.QuestionNumber);
            viewModel.NextQuestionCommand.Execute(null);
            Assert.AreEqual("When was Magna Carta siged?", viewModel.QuestionAnswers.Question);
            Assert.AreEqual(2, viewModel.QuestionNumber);
        }

        [Test]
        public void ShouldPublishQuizCompletedEvent()
        {
            var quizCompletedEvent = new QuizCompletedEvent();
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(ea => ea.GetEvent<QuizCompletedEvent>())
                .Returns(quizCompletedEvent);

            var resultViewModel = new ResultViewModel(eventAggregator.Object);
            quizCompletedEvent.Publish(DummyQuestionAnswers.LoadQuestionAnswers());
            Assert.AreEqual(true, resultViewModel.Results.Count > 0);
        }
    }
}
