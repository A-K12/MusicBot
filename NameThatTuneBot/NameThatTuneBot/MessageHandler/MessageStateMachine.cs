using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTuneBot.MessageHandler
{
    public class MessageStateMachine:MessageHandlerModule
    {
        public MessageStateMachine(IMusicTrackDatabase musicDatabase)
        {
            this.messageRegister = new MessageRegister();
            this.messageBuilder = new MessageBuilder(musicDatabase);
            this.messageHistory = new MessageHistory();
            this.statisticsManager = new StatisticsManager();
        }

        private async Task<Message> HandleMessageAsync(Message message)
        {
            var state = messageRegister.RegisterMessage(message);
            if (state is UserStates.SecondLevel)
            {
                return  await SecondLevel(message);
            }
            else
            {
                return await FirstLevel(message);
            }
        }
        private async Task<Message> FirstLevel(Message message)
        {
            if ("Start the game" == message.BasicText || "!Start" == message.BasicText)
            {
                messageRegister.SetState(message, UserStates.SecondLevel);
                statisticsManager.AddNewUser(message.User);
                return GetAndSaveSelectMessage(message);
            }

            if ("Game statistics" == message.BasicText || "!Stat" == message.BasicText)
            {
                return await messageBuilder.GetStatisticMessage(message);
            }
            return GetAndSaveMainMessage(message);
        }


        private Message GetAndSaveMainMessage(Message message)
        {
            var newMessage = messageBuilder.GetMainPage(message);
            messageHistory.AddMessage(newMessage);
            return newMessage;
        }

        private Message GetAndSaveSelectMessage(Message newMessage, Message pastMessage = null)
        {
            var message = messageBuilder.GetSelectPage(newMessage, pastMessage);
            messageHistory.AddMessage(message);
            return message;
        }

        private async Task<Message> SecondLevel(Message message)
        {
            if ("Stop the game" == message.BasicText || "!Stop" == message.BasicText)
            {
                messageRegister.SetState(message, UserStates.FirstLevel);
                return GetAndSaveMainMessage(message);
            }
            if (Regex.IsMatch(message.BasicText, @"^[1-4]$", RegexOptions.Multiline))
            {
                var pastMessage = messageHistory.GetMessage(message.User);
                var result = pastMessage.RightAnswer.ToString() == message.BasicText;
                statisticsManager.UpdateUserStatistics(message.User,result);
                return GetAndSaveSelectMessage(message,pastMessage);
            }
            if (message.BasicText == "Replace")
            {
                return ReplaceAndSaveSelectMessage(message);
            }
           
            return messageHistory.GetMessage(message.User);
        }

        private Message ReplaceAndSaveSelectMessage(Message message)
        {
            var newMessage = messageBuilder.ReplaceSelectMessage(message);
            messageHistory.AddMessage(newMessage);
            return newMessage;
        }

        protected override async Task Send(Message message)
        {
           await botMediator.Send(message, this);
        }

        public override async Task Receive(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message)); ;
            var newMessage = await HandleMessageAsync(message);
            await Send(newMessage);
        }

        internal void SetMessageRegister(IMessageRegister messageRegister)
        {
            this.messageRegister = messageRegister;
        }

        internal void SetMessageHistory(IMessageHistory messageHistory)
        {
            this.messageHistory = messageHistory;
        }

        internal void SetMessageBuilder(IMessageBuilder messageBuilder)
        {
            this.messageBuilder = messageBuilder;
        }

        internal void SetStatisticsManager(IStatisticsManager statisticsManager)
        {
            this.statisticsManager = statisticsManager;
        }

        private IMessageHistory messageHistory;
        private IMessageRegister messageRegister;
        private IMessageBuilder messageBuilder;
        private IStatisticsManager statisticsManager;
    }
}