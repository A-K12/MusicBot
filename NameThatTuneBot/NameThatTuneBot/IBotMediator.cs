using System.Threading.Tasks;
using NameThatTuneBot.MessageHandler;

namespace NameThatTuneBot
{
    public interface IBotMediator
    {
        Task Send(Message message, MessageHandlerModule module);
        void AddMessenger(MessengerApi messenger);
        void Start();
        void Stop();

    }
}