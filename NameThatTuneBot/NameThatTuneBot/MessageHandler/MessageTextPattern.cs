using System;
using System.Linq;
using NameThatTuneBot.Entities;

namespace NameThatTuneBot
{
    static class MessageTextPattern
    {

        public static string GetSelectPage(MusicTrack[] musicTracks)
        {
            var answers = musicTracks.Select(m => m.NameArtist + " - " + m.NameTrack).ToArray();
            if (answers.Length == 4)
            {
                return $"Выберите вариант ответа:\n1:{answers[0]};\n2:{answers[1]};\n3:{answers[2]};\n4:{answers[3]};";
            }
            else
            {
                throw new ArgumentException();
            }
        }
        public static string GetSelectPage(string[] answer)
        {
            Console.WriteLine("answer" + answer.Length.ToString());
            Console.WriteLine("answer" + answer[0] + answer[1] + answer[2] + answer[3]);
            if (answer.Length >= 4)
            {

                return $"Выберите возможные варианты:\n1:{answer[0]};\n2:{answer[1]};\n3:{answer[2]};\n4:{answer[3]};";
            }
            else
            {
                throw  new ArgumentException();
            }
            
            
        }
        public static string GetResultMessage(bool typeAnswer)
        {
            return "Вы ответили: " + (typeAnswer ? "верно :D" : "неверно :ꓷ") +"\n\n";
        }

        public static string GetStartMessage()
        {
            return "Игра началась!!!\n\n";
        }


        public static string GetMainPage()
        {
            return "Игра \"Угадай песню за 10 секунд\"!!!:";
        }
    }
}
