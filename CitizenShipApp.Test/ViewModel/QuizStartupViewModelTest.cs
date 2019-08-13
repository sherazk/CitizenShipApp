using Autofac;
using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.Models;
using CitizenShipApp.Startup;
using CitizenShipApp.ViewModels;
using Moq;
using NUnit.Framework;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Test.ViewModel
{
    [TestFixture]
    public class QuizStartupViewModelTest
    {
        IQuizStartupViewModel viewModel;
        [SetUp]
        public void Setup()
        {
            //var eventAggregatorMock = new Mock<IEventAggregator>();
            var quizSettingDataProviderMock = new Mock<IQuizStartupDataProvider>();
            quizSettingDataProviderMock.Setup(dp => dp.GetChapters())
                .Returns(new Result<IEnumerable<string>>()
                {
                    Data = new List<string>() { "Chapter One", "Chapter Two" },
                    IsSucess = true
                });
            viewModel = new QuizStartupViewModel(quizSettingDataProviderMock.Object);
        }

        [Test]
        public void ShouldGetListOfChaptersWhenQuizLoads()
        {
            Assert.AreEqual(2, viewModel.Chapters.Count);
            Assert.AreSame("Chapter One", viewModel.Chapters[0]);
        }

        [Test]
        public void ShouldGetSelectedChapterWhenQuizLoads()
        {
            Assert.AreSame("Chapter One", viewModel.SelectedChapter);
        }

        [Test]
        public void ShouldGetSelectedNumberWhenQuizLoads()
        {
            Assert.AreEqual(5, viewModel.SelectedNumber);
        }

        [Test]
        public void ShouldLoadQuizStartupSettings()
        {
            var quizStartup = viewModel.LoadQuizStartupSettings();
            Assert.IsNotNull(quizStartup);
        }


    }

    //public class QuizSettingDataProviderMock : IQuizStartupDataProvider
    //{
    //    public IResult<IEnumerable<string>> GetChapters()
    //    {
    //        IResult<IEnumerable<string>> result = new Result<IEnumerable<string>>();
    //        result.Data = new List<string>() { "Chapter One", "Chapter Two" };
    //        result.IsSucess = true;
    //        return result;
    //    }
    //}
}
