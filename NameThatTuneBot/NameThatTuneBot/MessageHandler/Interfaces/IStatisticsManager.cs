
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.MessageHandler.Interfaces
{
    public interface IStatisticsManager
    {
        void AddNewUser(User user);

        void UpdateUserStatistics(User user, bool re);
    }
}