
namespace NameThatTuneBot.Messengers
{
    public static class DiscordKeyboardBuilder
    {
        private const string MainKeyboard = "\n-----------------\n !Start - Начать игру\n !Stat -Игровая статистика";
        private const string SelectKeyboard = "\n-----------------\n !Stop - Закончить игру";
        public static string GetKeyboard(KeyboardTypes keyboard)
        {
            switch (keyboard)
            {
                case KeyboardTypes.MainKeyboard:
                    return MainKeyboard;
                case KeyboardTypes.KeyboardSelection:
                    return SelectKeyboard;
                default:
                    return "";
            }
        }
    }
}