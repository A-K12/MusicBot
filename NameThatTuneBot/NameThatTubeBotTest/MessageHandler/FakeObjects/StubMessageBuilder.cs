using System.Threading.Tasks;
using NameThatTuneBot;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTubeBotTest.FakeObjects
{
    public class StubMessageBuilder:IMessageBuilder
    {
        public  Message NewMessage { get; set; }
        public Message PastMessage { get; set; }
        public  Message ReturnMessage { get; set; }

        public Message GetMainPage(Message message)
        {
            return GetMessage(message, "MainPage");
        }

        private Message GetMessage(Message message, string text)
        {
            var newMessage = (Message)message.Clone();
            newMessage.BasicText = text;
            return newMessage;
        }

        public Message GetSelectPage(Message newMessage, Message pastMessage=null)
        {
            return GetMessage(newMessage, "SelectPage");
        }

        public Message ReplaceSelectMessage(Message message)
        {
            return GetMessage(message, "ReplacePage");
        }

        public Task<Message> GetStatisticMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}