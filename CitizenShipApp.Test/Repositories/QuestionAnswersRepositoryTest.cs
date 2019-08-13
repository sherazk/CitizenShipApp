using Autofac;
using CitizenShipApp.Data;
using CitizenShipApp.Models;
using CitizenShipApp.Startup;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Repositories.Test
{
    [TestFixture]
    public class QuestionAnswersRepositoryTest
    {
        IQuestionAnswersRepository<QuestionAnswerXmlRepository> sut;

        [SetUp]
        public void Setup()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();
            sut = container.Resolve<IQuestionAnswersRepository<QuestionAnswerXmlRepository>>();
        }

        [Test]
        public void ShouldGetChapters()
        {
            var result = sut.GetChapters();
            Assert.IsNotEmpty(result.Data);
        }

        [Test]
        public void ShouldLoadQuestions()
        {
            IResult<IEnumerable<QuestionAnswers>> result = sut.GetQuestionsByChapter(1);
            Assert.IsNotNull(result.Data);
        }

        [Test]
        public void ShouldLoadQuestionAnswersByName()
        {
            IResult<IEnumerable<QuestionAnswers>> result = sut.GetQuestionsByChapter("Canada’s Regions");
            Assert.IsNotNull(result.Data);
        }
    }
}
