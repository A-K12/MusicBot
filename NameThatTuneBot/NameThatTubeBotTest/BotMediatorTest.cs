using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using NameThatTubeBotTest.FakeObjects;
using NameThatTuneBot;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NameThatTubeBotTest
{
    [TestFixture]
    class BotMediatorTest
    {
        private BotMediator GetBotMediator()
        {
            var fakeDatabase = new FakeMusicDatabase();
            var botMediator = new BotMediator(fakeDatabase);
            return botMediator;
        }

        private Message GetTestMessage(string id, string text)
        {
            var user = new User {ChatId = id, MessengerClass = nameof(FakeMessengerApi)};
            return new Message(user) {BasicText = text};
        }


        [Test]
        public void Start_WhenCall_CallsMessenger()
        {
            var botMediator = GetBotMediator();
            var fakeMessenger = new FakeMessengerApi();
            botMediator.AddMessenger(fakeMessenger);

            botMediator.Start();

            Assert.True(fakeMessenger.CheckStart);
        }

        [Test]
        public void Stop_WhenCall_CallsMessenger()
        {
            var botMediator = GetBotMediator();
            var fakeMessenger = new FakeMessengerApi();
            botMediator.AddMessenger(fakeMessenger);

            botMediator.Stop();

            Assert.True(fakeMessenger.CheckStop);
        }

        [Test]
        public void Send_WhenCall_SendsToMessenger()
        {
            var botMediator = GetBotMediator();
            var fakeMessenger = new FakeMessengerApi();
            var fakeMessageHandler = new FakeMessageHandlerModule();
            var message = GetTestMessage("123123", "Test");
            botMediator.SetMessageStateMachine(fakeMessageHandler);
            botMediator.AddMessenger(fakeMessenger);

            botMediator.Send(message, fakeMessageHandler);

            Assert.AreSame(message, fakeMessenger.AddedMessage);
        }

        [Test]
        public void Send_WhenCall_SendsToHandler()
        {
            var botMediator = GetBotMediator();
            var fakeMessenger = new FakeMessengerApi();
            var fakeMessageHandler = new FakeMessageHandlerModule();
            var message = GetTestMessage("123123", "Test");
            botMediator.SetMessageStateMachine(fakeMessageHandler);
            botMediator.AddMessenger(fakeMessenger);

            botMediator.Send(message, fakeMessenger);

            Assert.AreSame(message, fakeMessageHandler.ReceiveMessage);
        }
    }
}
