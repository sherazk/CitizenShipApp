using Autofac;
using CitizenShipApp.Startup;
using CitizenShipApp.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Test
{
    [TestFixture]
    public class MainViewModelTest
    {
        private MainViewModel sut;

        [SetUp]
        public void Setup()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();
            sut = container.Resolve<MainViewModel>();
        }

        public void ShouldLoadQuizStartupSettings()
        {
            
        }

        [Test]
        public void CurrentViewModelShouldHaveAViewModelSet()   
        {
            sut.LoadView();
            Assert.IsNotNull(sut.CurrentViewModel);
        }

        [Test]
        public void ShouldHaveAListOfQuestionAnswers()
        {
            Assert.IsNotNull(sut.QuestionAnswers);
        }

        [Test]
        public void LoadQuestionAnswers()
        {
            sut.LoadsQuestionAnswers();
            Assert.That(sut.QuestionAnswers.Count,Is.GreaterThan(0));
        }

        [Test]
        public void ShouldSetQuestionAnswersViewModelWhenInvokeStartCommand()
        {
            sut.StartCommand.Execute(null);
            Assert.That(sut.CurrentViewModel, Is.TypeOf<QuestionAnswersViewModel>());
        }


    }
}
