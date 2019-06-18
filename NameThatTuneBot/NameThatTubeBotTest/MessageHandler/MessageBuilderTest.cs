using System;
using NameThatTubeBotTest.FakeObjects;
using NameThatTuneBot;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace NameThatTubeBotTest
{
    [TestFixture]
    public class MessageBuilderTest
    {
        private readonly MusicTrack track = new MusicTrack() { Id = 1, NameArtist = "Test", NameTrack = "Test" };


        [Test]
        public void GetMainPage_WhenCall_ReturnsMessage()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };
            var mainPageText = MessageTextPattern.GetMainPage();

            var newMessage = messageBuilder.GetMainPage(message);

            StringAssert.Contains(mainPageText,newMessage.BasicText);
        }

        [Test]
        public void GetSelectPage_OneMessage_ReturnsStartingMessage()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);
            var user = new User { Id = "123456", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = "Test" };
            var startText = MessageTextPattern.GetStartMessage();

            var newMessage = messageBuilder.GetSelectPage(message);

            StringAssert.Contains(newMessage.AdditionalText,  startText);
        }

        [TestCase("1", 1, true)]
        [TestCase("2", 1, false)]
        public void GetSelectPage_WithPastMessage_ReturnsMessage(string answer, int rightAnswer, bool expectedResult)
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);
            var user = new User { Id = "12345", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = answer, RightAnswer = rightAnswer };
            var resultText = MessageTextPattern.GetResultMessage(expectedResult);

            var newMessage = messageBuilder.GetSelectPage(message, message);

            StringAssert.Contains(newMessage.AdditionalText, resultText);
        }

        [Test]
        public void ReplaceSelectMessage_WhenCall_ReturnsMessage()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);
            var user = new User { Id = "12345", MessengerClass = this.GetType() };
            var message = new Message(user)
            {
                AdditionalText = "Past Answer",
                BasicText = "Test1"
            };

            var newMessage = messageBuilder.ReplaceSelectMessage(message);

            StringAssert.Contains(newMessage.AdditionalText, message.AdditionalText);
        }

        [Test]
        public void ReplaceSelectMessage_EmptyMessage_ThrowsException()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);

            var ex = Assert.Catch<Exception>(() => messageBuilder.ReplaceSelectMessage(null));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }



        [Test]
        public void GetMainPage_EmptyMessage_ThrowsException()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);
          
            var ex = Assert.Catch<Exception>(()=> messageBuilder.GetMainPage(null));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }


        [Test]
        public void GetSelectPage_EmptyMessages_ThrowsException()
        {
            var fakeDatabase = new FakeMusicDatabase { MusicTrack = track };
            var messageBuilder = new MessageBuilder(fakeDatabase);

            var ex = Assert.Catch<Exception>(() => messageBuilder.GetSelectPage(null, null));

            StringAssert.Contains("Value cannot be null", ex.Message);
        }

    }
}