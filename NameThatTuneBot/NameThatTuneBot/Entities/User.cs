using System;
using System.Collections.Generic;

namespace NameThatTuneBot.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string MessengerClass { get; set; }
        public string ChatId { get; set; }

        public UserStatistics UserStatistics { get; set; }

        private bool Equals(User x)
        {
            return x.ChatId.Equals(this.ChatId) && x.MessengerClass == this.MessengerClass;
        }

        public override bool Equals(Object x)
        {
            return this.Equals(x as User);
        }

        public override int GetHashCode()
        {
            return this.MessengerClass.GetHashCode() ^ this.ChatId.GetHashCode();
        }

    }
}