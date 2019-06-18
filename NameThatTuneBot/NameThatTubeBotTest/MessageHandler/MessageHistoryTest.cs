using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using NameThatTubeBotTest.FakeObjects;
using NameThatTuneBot;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;

namespace NameThatTubeBotTest
{
    [TestFixture]
    public class MessageHistoryTest
    {

        [Test]
        public void AddMessage_WhenCall_AddsMessageToDictionary()
        {
            var messageHistory = new MessageHistory();
            var user = new User {Id = "123456", MessengerClass = this.GetType()};
            var message = new Message(user) {BasicText = "Test"};

            messageHistory.AddMessage(message);
            var gettingMessage = messageHistory.GetMessage(user);

            Assert.AreSame(gettingMessage,message);
        }

        [Test]
        public void GetMessage_UnknownUser_TrowsException()
        {
            var messageHistory = new MessageHistory();
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test"};

            var ex = Assert.Catch<Exception>(() => messageHistory.GetMessage(user));
            

            StringAssert.Contains($"The user {user.Id} is not registered", ex.Message );
        }


        [Test]
        public void GetMessage_EmptyUser_TrowsException()
        {
            var messageHistory = new MessageHistory();
            var user = new User { Id = null, MessengerClass = null};

            var ex = Assert.Catch<Exception>(() => messageHistory.GetMessage(user));


            StringAssert.Contains("Value cannot be null", ex.Message);
        }

        [Test]
        public void AddMessage_EmptyMessage_TrowsException()
        {
            var messageHistory = new MessageHistory();
            
            var ex = Assert.Catch<Exception>(() => messageHistory.AddMessage(null));
            
            
            StringAssert.Contains("Value cannot be null",ex.Message );
        }

        [Test]
        public void AddMessage_EmptyUser_TrowsException()
        {
            var messageHistory = new MessageHistory();
            var user = new User { Id = null, MessengerClass = null };
            var message = new Message(user) { BasicText = "Test" };

            var ex = Assert.Catch<Exception>(() => messageHistory.AddMessage(message));


            StringAssert.Contains("Value cannot be null", ex.Message);
        }

    }
}
