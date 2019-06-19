using System.Threading.Tasks;

namespace NameThatTuneBot.MessageHandler.Interfaces
{
    public interface IMessageBuilder
    {
        Message GetMainPage(Message message);
        Message GetSelectPage(Message newMessage, Message pastMessage);
        Message ReplaceSelectMessage(Message message);
        Task<Message> GetStatisticMessage(Message message);
    }
}