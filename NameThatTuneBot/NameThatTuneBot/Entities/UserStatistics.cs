using System.ComponentModel.DataAnnotations;

namespace NameThatTuneBot.Entities
{
    public class UserStatistics
    {
        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}