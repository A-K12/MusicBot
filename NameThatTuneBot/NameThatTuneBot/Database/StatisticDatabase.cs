using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot.Database
{
    public static class StatisticDatabase
    {
        public static async Task AddUserStatistics(User user)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {

                if (await dataBase.Users.AnyAsync(o => o.ChatId == user.ChatId &&
                                                       o.MessengerClass == user.MessengerClass))
                {
                    throw new Exception($"User {user.ChatId} - {user.MessengerClass} already exists");
                }

                ;
                var newUser = user;
                await dataBase.Users.AddAsync(newUser);
                await dataBase.SaveChangesAsync();
                var userStat = new UserStatistics { UserId = newUser.Id, Id = newUser.Id};
                await dataBase.UserStatistics.AddAsync(userStat);
                await dataBase.SaveChangesAsync();
            }
        }

        public static async Task<bool> UserTrackExist(User user)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {
                return await dataBase.Users.AnyAsync(o => o.ChatId == user.ChatId &&
                                                    o.MessengerClass == user.MessengerClass);
            }
        }

        public static async Task<UserStatistics> GetUserStatistic(User user)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {
                var saveUser = await dataBase.Users.FirstOrDefaultAsync(u => u.ChatId == user.ChatId &&
                                                                             u.MessengerClass == user.MessengerClass);
                return await dataBase.UserStatistics.FirstOrDefaultAsync(us => us.Id == saveUser.Id);
            }
        }

        public static async Task UpdateUserStatistics(User user, bool answerType)
        {
            using (var dataBase = new NameThatTuneDatabase())
            {

                if (!await dataBase.Users.AnyAsync(o => o.ChatId == user.ChatId &&
                                                        o.MessengerClass == user.MessengerClass))
                {
                    throw new Exception($"User {user.ChatId} - {user.MessengerClass} does not exists");
                }

                var saveUser = await dataBase.Users.FirstOrDefaultAsync(u => u.ChatId == user.ChatId &&
                                                                             u.MessengerClass == user.MessengerClass);

                var userStat = await dataBase.UserStatistics.FirstOrDefaultAsync(us => us.Id == saveUser.Id);
                if (answerType)
                {
                    userStat.CorrectAnswers++;
                }
                else
                {
                    userStat.WrongAnswers++;
                }

                await dataBase.SaveChangesAsync();
            }
        }
    }
}