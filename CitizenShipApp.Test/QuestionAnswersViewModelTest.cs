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
    public class QuestionAnswersViewModelTest
    {
        QuestionAnswersViewModel sut;

        [SetUp]
        public void Setup()
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();
            sut = container.Resolve<QuestionAnswersViewModel>();
        }

        public void ViewModelShouldHaveQuestionAnswer()
        {
            //sut.QuestionAnsers
        }
    }
}
