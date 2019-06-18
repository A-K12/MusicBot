using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NameThatTuneBot.Messengers;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot
{ 
    public class Message:ICloneable
    {
        public User User { get; set; }
        public  MessageType MessageType { get; set; }
        public string AdditionalText { get; set; }
        public string BasicText { get; set; }
        public int RightAnswer { get; set; }
        public MusicTrack MusicTrack { get; set; }
        public KeyboardTypes KeyboardTypes { get; set; }

        public Message(User user)
        {
            this.User = user;
            AdditionalText = "";
            BasicText = "";
        }

        public object Clone()
        {
            return new Message(this.User)
            {
                BasicText = this.BasicText, KeyboardTypes = this.KeyboardTypes, MessageType = this.MessageType,
                MusicTrack = this.MusicTrack
            };
        }
    }
}
