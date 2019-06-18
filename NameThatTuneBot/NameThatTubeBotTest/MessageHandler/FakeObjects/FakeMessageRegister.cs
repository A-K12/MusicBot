using NameThatTuneBot;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTubeBotTest.FakeObjects
{
    public class FakeMessageRegister:IMessageRegister
    {
        public UserStates UserState { get; set; } = UserStates.FirstLevel;
        public Message  AddedMessage { get; set; }
        public  UserStates TestSet { get; set; }

        public UserStates RegisterMessage(Message message)
        {
            this.AddedMessage = message;
            return UserState;
        }

        public void SetState(Message message, UserStates states)
        {
            AddedMessage = message;
            TestSet = states;
        }

        public UserStates GetState(Message message)
        {
            return UserState;
        }
    }
}