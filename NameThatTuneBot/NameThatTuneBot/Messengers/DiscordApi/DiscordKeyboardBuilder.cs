
namespace NameThatTuneBot.Messengers
{
    public static class DiscordKeyboardBuilder
    {
        private const string MainKeyboard = "\n-----------------\n !Start - Начать игру";
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