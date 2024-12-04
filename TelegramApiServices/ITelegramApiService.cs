using Refit;
using System.Threading.Tasks;
using TelegramBot.TelegramApiServices;

public interface ITelegramApiService
{
    // Use Refit annotations for the Telegram Bot API endpoint
    [Post("/bot{botToken}/sendMessage")]
    Task<TelegramResponse> SendMessageAsync(
        [AliasAs("botToken")] string botToken,
        [AliasAs("chat_id")] long chatId,
        [AliasAs("text")] string text
    );
}
