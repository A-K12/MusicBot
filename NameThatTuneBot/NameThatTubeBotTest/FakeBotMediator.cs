using System.Threading.Tasks;
using NameThatTuneBot;

namespace NameThatTubeBotTest
{
    public class FakeBotMediator:IBotMediator
    {
        public bool CheckStart { get; set; } = false;
        public bool CheckStop { get; set; } = false;

        public MessageHandlerModule Module { get; set; }

        public Message AddedMessage { get; set; }

        public MessengerApi AddedMessenger { get; set; }

        public Task Send(Message message, MessageHandlerModule module)
        {
            this.AddedMessage = (Message)message.Clone();
            this.Module = module;
            return Task.CompletedTask;
        }

        public void AddMessenger(MessengerApi messenger)
        {
            this.AddedMessenger = messenger;
        }

        public void Start()
        {
            this.CheckStart = true;
        }

        public void Stop()
        {
            this.CheckStop = true;
        }
    }
}