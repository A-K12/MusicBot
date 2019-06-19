using NameThatTuneBot.Entities;
using NameThatTuneBot.Messengers;

namespace NameThatTuneBot.MessageHandler.Interfaces
{
    public interface IMessageHistory
    {
        void AddMessage(Message message);
        Message GetMessage(User user);
    }
}