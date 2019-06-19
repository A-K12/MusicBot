using System;
using System.Collections.Generic;
using System.Linq;
using NameThatTuneBot.Messengers;
using NameThatTuneBot;
using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTuneBot.MessageHandler
{
    public class MessageHistory:IMessageHistory
    {

        private readonly Dictionary<User, Message> messages;

        public MessageHistory()
        {
            this.messages = new Dictionary<User, Message>();
        }


        public void AddMessage(Message message)
        {
            if (message?.User.ChatId == null || message.User.MessengerClass == null)
            {
                throw new ArgumentNullException(nameof(message));
            }
            var id = message.User;
            lock (this.messages)
            {
                if (this.messages.ContainsKey(id))
                {
                    this.messages[id] = message;
                }
                else
                {
                    this.messages.Add(id, message);
                }
            }
        }

        public Message GetMessage(User user)
        {
            if(user.ChatId== null || user.MessengerClass == null) throw new ArgumentNullException(nameof(user));
            lock (messages)
            {
                if (messages.ContainsKey(user))
                {
                    return messages[user];
                }
                else
                {
                    throw new Exception($"The user {user.ChatId} is not registered");
                }
            }
        }
    }
}