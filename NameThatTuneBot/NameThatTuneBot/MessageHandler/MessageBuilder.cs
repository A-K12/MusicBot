using System;
using System.Threading.Tasks;
using NameThatTuneBot;
using NameThatTuneBot.Database;
using NameThatTuneBot.MessageHandler;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTuneBot.MessageHandler
{
    public class MessageBuilder: IMessageBuilder
    {
        public MessageBuilder(IMusicTrackDatabase musicDatabase)
        {
            this.musicDatabase = musicDatabase;
        }


        public Message GetMainPage(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var newMessage = (Message)message.Clone();
            newMessage.BasicText = MessageTextPattern.GetMainPage();
            newMessage.MessageType = MessageType.Simple;
            newMessage.KeyboardTypes = KeyboardTypes.MainKeyboard;
            return  newMessage;
        }

        private string GetAnswerText(Message newMessage, Message pastMessage)
        {
            if (pastMessage is null)
            {
                return MessageTextPattern.GetStartMessage();
            }
            else
            {
                var answer = newMessage.BasicText == pastMessage.RightAnswer.ToString();
                return MessageTextPattern.GetResultMessage(answer);
            }
        }

        public Message GetSelectPage(Message message, Message pastMessage=null)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var musicTracks = musicDatabase.GetRandomMusicTracksAsync(4).Result;
            var random = new Random();
            var rightAnswer = random.Next(0, 3);

            var selectMessage = (Message)message.Clone();
            selectMessage.MessageType = MessageType.Music;
            selectMessage.KeyboardTypes = KeyboardTypes.KeyboardSelection;
            selectMessage.MusicTrack = musicTracks[rightAnswer];
            selectMessage.RightAnswer = ++rightAnswer;
            selectMessage.AdditionalText = GetAnswerText(message, pastMessage);
            selectMessage.BasicText = MessageTextPattern.GetSelectPage(musicTracks);

            return selectMessage; 
        }

        public Message ReplaceSelectMessage(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var newMessage = GetSelectPage(message, message);
            newMessage.AdditionalText = message.AdditionalText;
            return newMessage;
        }



        private readonly IMusicTrackDatabase musicDatabase;
    }
}