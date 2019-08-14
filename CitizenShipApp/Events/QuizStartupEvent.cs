using CitizenShipApp.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenShipApp.Events
{
    public class QuizStartupEvent : PubSubEvent<QuizStartup>
    {
        public void Subscribe(QuizStartup quizStartup)
        {
            throw new NotImplementedException();
        }
    }
}
