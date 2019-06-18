using System.Threading.Tasks;
using NameThatTuneBot;

namespace BotTest.MockObjects
{
    public class FakeMessageHandlerModule: MessageHandlerModule
    {
        public Message SendMessage { get; set; }
        public Message ReceiveMessage { get; set; }

        protected override async Task Send(Message message)
        {
            await botMediator.Send(message, this);
        }

        public override async Task Receive(Message message)
        {
            this.ReceiveMessage = message;
            await  Send(SendMessage);
        }
    }
}