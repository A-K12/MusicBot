using System.Linq;
using System.Threading.Tasks;
using System.IO;
using BotTest.MockObjects;
using NameThatTuneBot;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;

namespace BotTest
{

    public class MessageHandlerTest
    {
        [Test]
        public void HandleMessageTest1()
        {
            var database = new FakeMusicDatabase();
            var stateMachine = new MessageStateMachine(database);

        }

        [Test]
        public void MessageHistoryTest()
        {
            var messageHistory = new MessageHistory();
            var user = new User {Id = "123456", MessengerClass = this.GetType()};
            var message = new Message {BasicText = "Test", User = user};

            messageHistory.AddMessage(message);
            var gettingMessage = messageHistory.GetMessage(user);

            Assert.AreSame(gettingMessage,message);
        }
    }
}
