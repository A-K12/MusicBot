using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;

namespace NameThatTuneBot.Messengers
{
    public class TelegramApi: MessengerApi
    {
        System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
        
        public TelegramApi(string authToken)
        {
            client = new TelegramBotClient(authToken);
            client.OnMessage += BotOnMessageReceived;
            client.OnReceiveError += BotOnReceiveError;
        }

        public override async Task StartReceivingAsync()
        {
            await Task.Run(()=>StartReceiving());
        }

        private void StartReceiving()
        {
            try
            {
                var me = client.GetMeAsync().Result;
                System.Console.Title = me.Username;
                client.StartReceiving();
                System.Console.WriteLine($"Telegram:Start listening for @{me.Username}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Telegram:Error connection");
            }
        }

        public override Task StopReceivingAsync()
        {
            System.Console.WriteLine($"Telegram:Stop listening");
            return Task.Run(() => client.StopReceiving());
        }

        public override async Task SendSimpleMessageAsync(string id, string text, KeyboardTypes keyboardType)
        {
            if (keyboardType == KeyboardTypes.None)
            {
                await client.SendTextMessageAsync(id, text);
            }
            else
            {
                var keyboard = TelegramKeyboardBuilder.GetKeyboard(keyboardType);
                await client.SendTextMessageAsync(id, text, replyMarkup: keyboard);
            }

            myStopwatch.Stop();
            Console.Out.WriteLine("myStopwatch = {0}", myStopwatch.ElapsedMilliseconds);//остановить
        }

        public override  async Task SendMusicMessageAsync(string id, string text, MusicTrack track, KeyboardTypes keyboardType)
        {
            var musicVersion = track.MusicVersions.SingleOrDefault(t => t.Extension == "ogg");
            if (musicVersion == null)
            {
                throw new ArgumentNullException($"MusicVersion of track {track.Id} is null");
            }
            var keyboard = TelegramKeyboardBuilder.GetKeyboard(keyboardType);
            const int numberOfRetries = 3;
            const int delayOnRetry = 1000;
            
            for (var i=0; i< numberOfRetries; i++)
            {
                try
                {
                    var stream = new FileStream(musicVersion.TrackPath, FileMode.Open);
                    var inputFile = new InputOnlineFile(stream);
                    await client.SendVoiceAsync(id, inputFile, caption: text, replyMarkup: keyboard);
                    stream.Close();
                    break;
                }
                catch 
                {
                    if (i <= numberOfRetries)
                    {
                        Console.Out.WriteLine("track is busy = {0}", track.Id);
                        Task.Delay(delayOnRetry);
                    }
                    else
                    {
                        throw new Exception("Music file is invalid");
                    }
                    
                }
            }
            myStopwatch.Stop();
            Console.Out.WriteLine("myStopwatch = {0}", myStopwatch.ElapsedMilliseconds);//остановить
        }




        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.Out.WriteLine("sender = {0}", sender);

        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            myStopwatch.Reset();
            myStopwatch.Start();
            
            var user = new User
            {
                Id = e.Message.Chat.Id.ToString(),
                MessengerClass = typeof(TelegramApi)
            };
            var message = new Message()
            { 
                BasicText = e.Message.Text,
                User= user
            };
            await Send(message);
        }


        public override async Task SendMessage(Message message)
        {
            var textMessage = message.AdditionalText + message.BasicText;
            if (message.MessageType == MessageType.Music)
            {
                await SendMusicMessageAsync(message.User.Id, textMessage, message.MusicTrack, message.KeyboardTypes);
            }
            else
            {
                await SendSimpleMessageAsync(message.User.Id, textMessage , message.KeyboardTypes);
            }
           
        }

        protected override async Task Send(Message message)
        {
           await botMediator.Send(message, this);
        }


        public override async Task Receive(Message message)
        {
            if(message is null) throw new ArgumentNullException(nameof(message)); ;
            try
            {
               await SendMessage(message as Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var newMessage = (Message)message.Clone();
                newMessage.BasicText= "Replace";
                await Send(message);
            }
          
        }

        private TelegramBotClient client;
    }
}