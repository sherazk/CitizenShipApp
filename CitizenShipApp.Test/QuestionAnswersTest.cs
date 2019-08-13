using CitizenShipApp.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Test
{
    [TestFixture]
    public class QuestionAnswersTest
    {
        QuestionAnswers sut;
        [SetUp]
        public void Setup()
        {
            sut = new QuestionAnswers();
        }

        [Test]
        public void ShouldHaveAQuestion()
        {
            sut.SetQuestion("Whats My Name?");
            Assert.IsNotNull(sut.Question);
        }

        [Test]
        public void QuestionShouldHaveAnswers()
        {
            Assert.IsNotNull(sut.Answers);
        }

        [Test]
        public void QuestionShouldBeAnswered()
        {
            sut.IsQuestionAnswered = true;
            Assert.IsTrue(sut.IsQuestionAnswered);
        }

        [Test]
        public void ShouldBeAbleToSetAnswersForAQuestion()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 4" }
            };
            sut.SetAnswers(answers);
            Assert.That(sut.Answers, Is.Not.Null);
        }

        [Test]
        public void AnswersCannotBeMoreThanFour()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 4" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 5" }
           };
            Assert.Throws<Exception>(() => sut.SetAnswers(answers));
        }

        [Test]
        public void AnswersCannotBeLessThanFour()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
            };
            Assert.Throws<Exception>(() => sut.SetAnswers(answers));
        }

        [Test]
        public void ThrowException_QuestionShouldHaveAtleastOneCorrectAnswer()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = false, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 4" }
            };
            Assert.Throws<Exception>(() => sut.SetAnswers(answers));
        }

        [Test]
        public void ThrowException_QuestionShouldNotHaveMoreThanOneCorrectAnswer()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = true, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 4" }
            };
            Assert.Throws<Exception>(() => sut.SetAnswers(answers));
        }

        [Test]
        public void AtLeastOneAnswerShouldBeCorrect()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" }
            };
            Assert.AreEqual(true, answers.Any(x => x.IsCorrect == true));
        }

        [Test]
        public void OnlyOneAnswerCanBeCorrect()
        {
            List<Answer> answers = new List<Answer>()
            {
                new Answer() { IsCorrect = true, AnswerValue = "Answer 1" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 2" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 3" },
                new Answer() { IsCorrect = false, AnswerValue = "Answer 4" }
            };
            Assert.AreEqual(1, answers.Where(x => x.IsCorrect == true).Count());
        }
    }
}
