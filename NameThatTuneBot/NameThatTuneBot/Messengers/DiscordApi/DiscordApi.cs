using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.Messengers
{
    public class DiscordApi:MessengerApi
    {
        private readonly DiscordSocketClient client;
        private string token;
        public DiscordApi(string token) 
        {
            client = new DiscordSocketClient();
            this.token = token;
            client.Log += LogAsync;
            client.Ready += ReadyAsync;
            client.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage arg)
        {
            if (arg.Author.Id == client.CurrentUser.Id)
                return;

            var user = new User
            {
                ChatId = arg.Channel.Id.ToString(), MessengerClass = nameof(DiscordApi)
            };
            var message = new Message(user)
            {
                BasicText = arg.Content
            };
            await Send(message);
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"{client.CurrentUser} is connected!");

            return Task.CompletedTask;
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        protected override async Task Send(Message message)
        {
            await botMediator.Send(message, this);
        }

        public override async Task SendMessage(Message message)
        {
            var textMessage = message.AdditionalText + message.BasicText;
            if (message.MessageType == MessageType.Music)
            {
                await SendMusicMessageAsync(message.User.ChatId,textMessage,message.MusicTrack, message.KeyboardTypes);
            }
            else
            {
                await SendSimpleMessageAsync(message.User.ChatId, textMessage, message.KeyboardTypes);
            }
        }

        public override async Task Receive(Message message)
        {
            if (message is null) throw new ArgumentNullException(nameof(message));
            try
            {
                await SendMessage(message as Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var newMessage = (Message)message.Clone();
                newMessage.BasicText = "Replace";
                await Send(message);
            }
            
        }

        public override async Task StartReceivingAsync()
        {
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
        }

        public override async Task StopReceivingAsync()
        {
            await client.StopAsync();
        }

        public override async Task SendSimpleMessageAsync(string id, string text, KeyboardTypes keyboardType)
        {
            if (ulong.TryParse(id, out var userId)&&!string.IsNullOrEmpty(text))
            {
                var sendText = text + DiscordKeyboardBuilder.GetKeyboard(keyboardType);
                if (client.GetChannel(userId) is ISocketMessageChannel userChannel)
                    await userChannel.SendMessageAsync(sendText);
            }
            else
            {
                throw  new ArgumentException("Chat or text is invalid");
            }
        }

        public override async Task SendMusicMessageAsync(string id, string text, MusicTrack track, KeyboardTypes keyboardType)
        {
            var musicVersion = track.MusicVersions.SingleOrDefault(t => t.Extension == "ogg");
            var sendText = text + DiscordKeyboardBuilder.GetKeyboard(keyboardType);
            if (musicVersion == null)
            {
                throw new ArgumentNullException($"В базе нет версии ogg трека - {track.Id}");
            }
            if (ulong.TryParse(id, out var userId))
            {
                if (client.GetChannel(userId) is ISocketMessageChannel userChannel)
                    await userChannel.SendFileAsync(musicVersion.TrackPath,sendText);
            }
            else
            {
                throw new ArgumentException("Chat or text is invalid");
            }
        }
    }
}