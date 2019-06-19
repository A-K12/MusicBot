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
            newMessage.BasicText = MessageTextPatterns.GetMainPage();
            newMessage.MessageType = MessageType.Simple;
            newMessage.KeyboardTypes = KeyboardTypes.MainKeyboard;
            return  newMessage;
        }

        public Message ReplaceSelectMessage(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            var newMessage = GetSelectPage(message, message);
            newMessage.AdditionalText = message.AdditionalText;
            return newMessage;
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
            selectMessage.BasicText = MessageTextPatterns.GetSelectPage(musicTracks);

            return selectMessage; 
        }

        private string GetAnswerText(Message newMessage, Message pastMessage)
        {
            if (pastMessage is null)
            {
                return MessageTextPatterns.GetStartMessage();
            }
            else
            {
                var answer = newMessage.BasicText == pastMessage.RightAnswer.ToString();
                return MessageTextPatterns.GetResultMessage(answer);
            }
        }


        public async Task<Message> GetStatisticMessage(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            var newMessage = (Message)message.Clone();

            var result = await StatisticDatabase.UserTrackExist(message.User);
            if (result)
            {
                var userStat = await StatisticDatabase.GetUserStatistic(message.User);
                newMessage.BasicText =
                    MessageTextPatterns.GetStatisticPage(userStat.WrongAnswers, userStat.CorrectAnswers);
            }
            else
            {
                newMessage.BasicText = MessageTextPatterns.GetEmptyStatisticPage();
            }
            newMessage.MessageType = MessageType.Simple;
            newMessage.KeyboardTypes = KeyboardTypes.MainKeyboard;
            return newMessage;
        }


        private readonly IMusicTrackDatabase musicDatabase;
    }
}