using Autofac;
using CitizenShipApp.Data;
using CitizenShipApp.DataProvider;
using CitizenShipApp.ViewModels;
using CitizenShipApp.Views;
using Prism.Events;

namespace CitizenShipApp.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();
            //Events
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            //Views
            builder.RegisterType<MainView>().AsSelf();
            builder.RegisterType<QuizStartupView>().AsSelf();
            builder.RegisterType<QuestionAnswersView>().AsSelf().SingleInstance();
            builder.RegisterType<ResultView>().AsSelf().SingleInstance();

            //View Model dependency injections
            builder.RegisterType<MainViewModel>().SingleInstance();//IQuizStartupViewModel
            builder.RegisterType<QuizStartupViewModel>().As< IQuizStartupViewModel>().SingleInstance();
            builder.RegisterType<QuestionAnswersViewModel>().As<IQuestionAnswersViewModel>().SingleInstance();
            builder.RegisterType<ResultViewModel>().As<IResultViewModel>().SingleInstance();
            builder.RegisterType<QuizStartupDataProvider>().As<IQuizStartupDataProvider>().SingleInstance();
            builder.RegisterType<QuestionAnswerDataProvider>().As<IQuestionAnswerDataProvider>().SingleInstance();

            //Repositories
            builder.RegisterType<QuestionAnswersRepository>().As<IQuestionAnswersRepository<QuestionAnswersRepository>>().SingleInstance();
            builder.RegisterType<QuestionAnswerXmlRepository>().As<IQuestionAnswersRepository<QuestionAnswerXmlRepository>>().SingleInstance();

            return builder.Build();
        }
    }
}
