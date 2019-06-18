using NameThatTuneBot;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace BotTest.MockObjects
{
    public class FakeMessageBuilder:IMessageBuilder
    {
        public  Message NewMessage { get; set; }
        public Message PastMessage { get; set; }
        public  Message ReturnMessage { get; set; }
        public Message GetMainPage(Message message)
        {
            NewMessage = message;
            return ReturnMessage;
        }

        public Message GetSelectPage(Message newMessage, Message pastMessage)
        {
            NewMessage = newMessage;
            PastMessage = pastMessage;
            return ReturnMessage;
        }

        public Message ReplaceSelectMessage(Message message)
        {
            NewMessage = message;
            return ReturnMessage;
        }
    }
}