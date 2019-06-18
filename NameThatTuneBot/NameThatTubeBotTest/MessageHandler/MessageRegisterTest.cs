using System;
using NameThatTuneBot;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;

namespace NameThatTubeBotTest
{
    [TestFixture]
    public class MessageRegisterTest
    {
        [Test]
        public void RegisterMessage_UnknownMessage_ReturnsFirstLevelState()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };

            var states = messageRegister.RegisterMessage(message);

            Assert.AreEqual( UserStates.FirstLevel, states);
        }

        [Test]
        public void SetState_UnknownUser_SetStateToUser()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };

            messageRegister.SetState(message, UserStates.SecondLevel);
            var states =  messageRegister.RegisterMessage(message);

            Assert.AreEqual(UserStates.SecondLevel, states);
        }

        [Test]
        public void GetState_WhenCall_SetStateToUser()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };
            messageRegister.RegisterMessage(message);

            messageRegister.SetState(message, UserStates.SecondLevel);
            var states = messageRegister.GetState(message);

            Assert.AreEqual(UserStates.SecondLevel, states);
        }


        [Test]
        public void GetState_UnknownMessage_ThrowException()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };

            var ex = Assert.Catch<Exception>(() => messageRegister.GetState(message));


            StringAssert.Contains("Message doesn't register", ex.Message);
        }


        [Test]
        public void RegisterMessage_EmptyUser_TrowsException()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "null", MessengerClass = null };
            var message = new Message(user) { BasicText = "Test" };

            var ex = Assert.Catch<Exception>(() => messageRegister.RegisterMessage(message));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }

        [Test]
        public void SetState_EmptyUser_TrowsException()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "null", MessengerClass = null };
            var message = new Message(user) { BasicText = "Test" };

            var ex = Assert.Catch<Exception>(() => messageRegister.SetState(message,UserStates.SecondLevel));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }

        public void GetState_EmptyUser_TrowsException()
        {
            var messageRegister = new MessageRegister();
            var user = new User { Id = "null", MessengerClass = null };
            var message = new Message(user) { BasicText = "Test" };

            var ex = Assert.Catch<Exception>(() => messageRegister.GetState(message));

            StringAssert.Contains("Message doesn't register", ex.Message);
        }

        [Test]
        public void RegisterMessage_EmptyMessage_TrowsException()
        {
            var messageRegister = new MessageRegister();

            var ex = Assert.Catch<Exception>(() => 
                messageRegister.RegisterMessage(null));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }
    }
}