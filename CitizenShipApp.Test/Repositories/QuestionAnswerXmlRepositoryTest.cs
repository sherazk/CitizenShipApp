using Autofac;
using CitizenShipApp.Data;
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
    public class QuestionAnswerXmlRepositoryTest
    {
        private IQuestionAnswersRepository<QuestionAnswerXmlRepository> sut;
        [SetUp]
        public void Setup()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();
            sut = container.Resolve<IQuestionAnswersRepository<QuestionAnswerXmlRepository>>();
        }

        [Test]
        public void ShouldLoadChapters()
        {
            var result = sut.GetChapters();
            Assert.That(result.IsSucess, Is.True);
        }

        [Test]
        public void ShouldGetResultByChapterNameAndNumberOfQuestions()
        {
            var result = sut.GetNumberOfQuestionsByChapter("Rights and Responsibilities of Citizenship", 5);
            Assert.That(result.Data.Count(), Is.EqualTo(5));
        }

        public void ShouldGetQuestionAnswersByChapterId()
        {

        }

        [Test]
        public void ShouldGetQuestionAnswersByChapterName()
        {
            var result = sut.GetQuestionsByChapter("Canada’s Regions");
            Assert.That(result.Data, Is.All.Not.Null);
        }
    }
}
