public static class BotSettings
{
    private static readonly string _botToken;
    private static readonly long _chatId;

    static BotSettings()
    {
        _botToken = "7548615523:AAE2dUYR0aokZ09vgdYv8PunfqNaPGTGqYs";
        _chatId = 856003986;
    }

    public static string BotToken => _botToken;
    public static long ChatId => _chatId;
}
