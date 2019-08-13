using CitizenShipApp.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Models.Test
{
    [TestFixture]
    public class QuizTest
    {
        Quiz sut;
        [SetUp]
        public void Setup()
        {
            sut = new Quiz();
        }

        [Test]
        public void QuizShouldHaveAListOfQuestionAnswers()
        {
            Assert.IsNotNull(sut.QuestionAnswers);
        }

        [Test]
        public void ThrowAnException_IfQuisQuestionAnswerIsLessThanOne()
        {
            List<QuestionAnswers> questionAnswers = new List<QuestionAnswers>();
            Assert.Throws<Exception>(() => sut.SetQuestionAnswers(questionAnswers));
        }

        [Test]
        public void ShouldSetQuestionAnswers()
        {
            List<QuestionAnswers> questionAnswers = new List<QuestionAnswers>();
            questionAnswers = LoadQuestionAnswers();
            sut.SetQuestionAnswers(questionAnswers);
            Assert.That(sut.QuestionAnswers, Is.Not.Null);
        }

        [Test]
        public void ShouldGetNumberOfCorrectAnswers()
        {
            List<QuestionAnswers> questionAnswers = LoadQuestionAnswers();
            sut.SetQuestionAnswers(questionAnswers);
            Assert.That(sut.GetNumberOfCorrectAnswers(), Is.EqualTo(2));
        }

        [Test]
        public void ShouldCalculatePercentageOfCorrectAnswers()
        {
            List<QuestionAnswers> questionAnswers = LoadQuestionAnswers();
            sut.SetQuestionAnswers(questionAnswers);
            Assert.That(sut.CalCulatePercentage(), Is.EqualTo(66.67));
        }

        private List<QuestionAnswers> LoadQuestionAnswers()
        {
            List<QuestionAnswers> questionAnswers = new List<QuestionAnswers>();
            QuestionAnswers question1 = new QuestionAnswers();
            question1.SetQuestion("What is name of the capital of Ontrario?");
            question1.Result = true;
            question1.SetAnswers(
                new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue="Toronto"},
                new Answer() { IsCorrect = false, AnswerValue = "Qubec" },
                new Answer() { IsCorrect = false, AnswerValue = "Minitoba" },
                new Answer() { IsCorrect = false, AnswerValue = "New Brunswick" }
            });

            QuestionAnswers question2 = new QuestionAnswers();
            question2.SetQuestion("When was Magna Carta siged?");
            question2.Result = true;
            question2.SetAnswers(
                new List<Answer>()
            {
                new Answer() {  IsCorrect = false, AnswerValue="1218"},
                new Answer() { IsCorrect = false, AnswerValue = "1261" },
                new Answer() { IsCorrect = true, AnswerValue = "1215" },
                new Answer() { IsCorrect = false, AnswerValue = "1230" }
            });

            QuestionAnswers question3 = new QuestionAnswers();
            question3.SetQuestion("Who is the prime minister of Canada?");
            question3.Result = false;
            question3.SetAnswers(
                new List<Answer>()
            {
                new Answer() {  IsCorrect = false, AnswerValue="John Wich"},
                new Answer() { IsCorrect = true, AnswerValue = "Justin Trudu" },
                new Answer() { IsCorrect = false, AnswerValue = "John Terry" },
                new Answer() { IsCorrect = false, AnswerValue = "Total Stranger" }
            });

            questionAnswers.Add(question1);
            questionAnswers.Add(question2);
            questionAnswers.Add(question3);
            return questionAnswers;

        }
    }
}
