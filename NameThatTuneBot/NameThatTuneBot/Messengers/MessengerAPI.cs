using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot
{
    public abstract class MessengerApi:MessageHandlerModule 
    {
        public abstract Task StartReceivingAsync();
        public abstract Task StopReceivingAsync();
        public abstract Task SendSimpleMessageAsync(string id, string text, KeyboardTypes keyboardType);
        public abstract Task SendMusicMessageAsync(string id, string text, MusicTrack track, KeyboardTypes keyboardType);
        public abstract Task SendMessage(Message message);
    }
}
