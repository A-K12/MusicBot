using Telegram.Bot.Types.ReplyMarkups;

namespace NameThatTuneBot.Messengers
{
    public static class TelegramKeyboardBuilder
    {
        public static ReplyKeyboardMarkup GetKeyboard(KeyboardTypes keyboard)
        {
            switch (keyboard)
            {
                case KeyboardTypes.MainKeyboard:
                    return GetMainTypeKeyboard();
                case KeyboardTypes.KeyboardSelection:
                    return GetKeyboardSelection();
                default:
                    return new ReplyKeyboardMarkup();
            }
        }

        public static ReplyKeyboardMarkup GetMainTypeKeyboard()
        {
            return new ReplyKeyboardMarkup(new[]
                    {
                        new [] 
                        {
                            new KeyboardButton("Start the game")
                        },
                        new []
                        {
                            new KeyboardButton("Game statistics")
                        },
                    });
        }

        public static ReplyKeyboardMarkup GetKeyboardSelection()
        {
            return new ReplyKeyboardMarkup(new[]
                {
                        new [] 
                    {
                        new KeyboardButton("1"),
                        new KeyboardButton("2"),
                    },
                        new [] // last row
                    {
                        new KeyboardButton("3"),
                        new KeyboardButton("4"),
                    },
                    new []
                    {
                        new KeyboardButton("Stop the game"),
                      
                    }
                });

        }
    }
}
