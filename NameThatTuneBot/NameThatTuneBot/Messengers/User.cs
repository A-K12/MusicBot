using System;
using System.Collections.Generic;

namespace NameThatTuneBot.Messengers
{
    public struct User: IEqualityComparer<User>
    {
        public Type MessengerClass { get; set; }
        public string Id { get; set; }


        public bool Equals(User x, User y)
        {
            return x.Id.Equals(y.Id) && x.MessengerClass == y.MessengerClass;
        }

        public  int GetHashCode(User obj)
        {
            return obj.Id.GetHashCode() + obj.MessengerClass.GetHashCode();
        }
    }
}