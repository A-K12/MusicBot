namespace NameThatTuneBot.MessageHandler.Interfaces
{
    public interface IMessageRegister
    {
        UserStates RegisterMessage(Message message);
        void SetState(Message message, UserStates states);
        UserStates GetState(Message message);
    }
}