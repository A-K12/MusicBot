using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NameThatTuneBot;
using NameThatTuneBot.Database;
using NameThatTuneBot.MessageHandler;

namespace NameThatTuneBot
{
    public class BotMediator:IBotMediator
    {
        private Dictionary<Type, MessengerApi> messengers;
        private MessageHandlerModule messageHandler;

        public BotMediator(IMusicTrackDatabase musicDatabase)
        {
            this.messengers = new Dictionary<Type, MessengerApi>();
            this.messageHandler = new MessageStateMachine(musicDatabase);
            this.messageHandler.AddMediator(this);
        }

        public async Task Send(Message message, MessageHandlerModule module)
        {
            switch (module)
            {
                case MessengerApi _:
                    await messageHandler.Receive(message);
                    break;
                case MessageStateMachine _: 
                    await messengers[message.User.MessengerClass].Receive(message);
                    break;
            }
        }

        public void AddMessenger(MessengerApi messenger)
        {
            messenger.AddMediator(this);
            this.messengers.Add(messenger.GetType(),messenger);
        }

        public void SetMessageStateMachine(MessageStateMachine messageStateMachine)
        {
            this.messageHandler = messageStateMachine;
        }

        public async  void Start()
        {
            foreach (var messenger in messengers)
            {
                await messenger.Value.StartReceivingAsync();
            }
        }

        internal void SetStateMachine(MessageHandlerModule stateMachine)
        {
            this.messageHandler = stateMachine;
        }

        public async void Stop()
        {
            foreach (var messenger in messengers)
            {
                await messenger.Value.StopReceivingAsync();
            }
        }
    }
}