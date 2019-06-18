using System.Threading.Tasks;
using NameThatTubeBotTest.FakeObjects;
using NameThatTuneBot;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.Messengers;
using NUnit.Framework;

namespace NameThatTubeBotTest
{
    [TestFixture]
    public class MessageStateMachineTest
    {
        private MessageStateMachine GetMessageHandler()
        {
            var fakeDatabase = new FakeMusicDatabase();
            return new MessageStateMachine(fakeDatabase);
        }

        [TestCase("123123", UserStates.FirstLevel, "MainPage")]
        [TestCase("!Start", UserStates.FirstLevel, "SelectPage")]
        [TestCase("Start the game", UserStates.FirstLevel, "SelectPage")]
        [TestCase("123123", UserStates.SecondLevel, "HistoryPage")]
        [TestCase("1", UserStates.SecondLevel, "SelectPage")]
        [TestCase("2", UserStates.SecondLevel, "SelectPage")]
        [TestCase("3", UserStates.SecondLevel, "SelectPage")]
        [TestCase("4", UserStates.SecondLevel, "SelectPage")]
        [TestCase("5", UserStates.SecondLevel, "HistoryPage")]
        [TestCase("!Stop", UserStates.SecondLevel, "MainPage")]
        [TestCase("Stop the game", UserStates.SecondLevel, "MainPage")]
        [TestCase("Replace", UserStates.SecondLevel, "ReplacePage")]
        public void Receive_SendMessage_ReturnsPage(string text, UserStates userStates, string expectedPage)
        {
            var messageHandler = GetMessageHandler();
            var fakeMediator = new FakeBotMediator();
            var stubMessageBuilder = new StubMessageBuilder();
            var fakeRegister = new FakeMessageRegister();
            var fakeHistory = new FakeMessageHistory();
            messageHandler.SetMessageBuilder(stubMessageBuilder);
            messageHandler.AddMediator(fakeMediator);
            fakeRegister.UserState = userStates;
            messageHandler.SetMessageRegister(fakeRegister);
            var user = new User { Id = "12345", MessengerClass = this.GetType() };
            var message = new Message(user) { BasicText = text };
            var pastMessage= new Message(user) { BasicText = "HistoryPage"};
            fakeHistory.ReturnMessage = pastMessage;
            messageHandler.SetMessageHistory(fakeHistory);

            messageHandler.Receive(message).Wait();

            StringAssert.Contains(expectedPage, fakeMediator.AddedMessage.BasicText);
        }

    }
}