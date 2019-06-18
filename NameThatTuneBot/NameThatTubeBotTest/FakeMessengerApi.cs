﻿using System.Threading.Tasks;
using NameThatTuneBot;
using NameThatTuneBot.Entities;
using Telegram.Bot.Requests;

namespace BotTest.MockObjects
{
    public class FakeMessengerApi:MessengerApi
    {
        public Message receiveMessage;
        protected override async Task Send(Message message)
        {
            await botMediator.Send(message, this);
        }

        public override Task Receive(Message message)
        {
            this.receiveMessage = message;
            return  Task.CompletedTask;
        }

        public override Task StartReceivingAsync()
        {
            return Task.CompletedTask;
        }

        public override Task StopReceivingAsync()
        {
            return Task.CompletedTask;
        }

        public override Task SendSimpleMessageAsync(string id, string text, KeyboardTypes keyboardType)
        {
            throw new System.NotImplementedException();
        }

        public override Task SendMusicMessageAsync(string id, string text, MusicTrack track, KeyboardTypes keyboardType)
        {
            throw new System.NotImplementedException();
        }

        public override Task SendMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}