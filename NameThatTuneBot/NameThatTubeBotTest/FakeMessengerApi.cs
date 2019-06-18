using System.Threading.Tasks;
using NameThatTuneBot;
using NameThatTuneBot.Entities;
using Telegram.Bot.Requests;

namespace NameThatTubeBotTest
{
    public class FakeMessengerApi:MessengerApi
    {
        public bool CheckStart { get; set; } = false;
        public bool CheckStop { get; set; } = false;

        public MessageHandlerModule Module { get; set; }

        public Message AddedMessage { get; set; }

        protected override async Task Send(Message message)
        {
            await botMediator.Send(message, this);
        }

        public override Task Receive(Message message)
        {
            this.AddedMessage = message;
            return  Task.CompletedTask;
        }

        public override Task StartReceivingAsync()
        {
            this.CheckStart = true;
            return Task.CompletedTask;
        }

        public override Task StopReceivingAsync()
        {
            this.CheckStop = true;
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