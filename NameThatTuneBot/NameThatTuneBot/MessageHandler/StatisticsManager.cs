using NameThatTuneBot.Database;
using NameThatTuneBot.Entities;
using NameThatTuneBot.MessageHandler.Interfaces;

namespace NameThatTuneBot.MessageHandler
{
    public class StatisticsManager:IStatisticsManager
    {
        public async void AddNewUser(User user)
        {
            var result = await StatisticDatabase.UserTrackExist(user);
            if (!result)
                await StatisticDatabase.AddUserStatistics(user);

        }

        public async void UpdateUserStatistics(User user, bool answerType)
        {
            await StatisticDatabase.UpdateUserStatistics(user, answerType);
        }
    }
}