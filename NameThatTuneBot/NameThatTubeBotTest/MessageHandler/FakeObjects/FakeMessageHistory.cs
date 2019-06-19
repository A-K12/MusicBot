using NameThatTuneBot;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler.Interfaces;
using NameThatTuneBot.Messengers;

namespace NameThatTubeBotTest.FakeObjects
{
    public class FakeMessageHistory:IMessageHistory
    {
        public Message AddedMessage { get; set; }
        public Message AddedUser { get; set; }
        public Message ReturnMessage { get; set; }
        public void AddMessage(Message message)
        {
            this.AddedMessage = message;
        }

        public Message GetMessage(User user)
        {
            return this.ReturnMessage;
        }
    }
}