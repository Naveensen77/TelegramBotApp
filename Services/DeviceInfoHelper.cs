using Android.Locations;
using Android.Gms.Location;
using System.Threading.Tasks;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;

public class DeviceInfoHelper
{
    // Get the device name
    public static string GetDeviceName()
    {
        return Build.Model;
    }

    // Get the total RAM of the device
    public static string GetTotalRAM()
    {
        var memoryInfo = new ActivityManager.MemoryInfo();
        var activityManager = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
        activityManager.GetMemoryInfo(memoryInfo);
        return (memoryInfo.TotalMem / (1024 * 1024 * 1024)).ToString("0.00") + " GB"; // In GB
    }

    // Get the total storage (ROM) of the device
    public static string GetTotalStorage()
    {
        StatFs statFs = new StatFs(Android.OS.Environment.ExternalStorageDirectory.Path);
        long totalBytes = statFs.BlockCountLong * statFs.BlockSizeLong;
        return (totalBytes / (1024 * 1024 * 1024)).ToString("0.00") + " GB"; // In GB
    }

    // Get the current device location (latitude, longitude) and link
    public static async Task<string> GetLocationAsync()
    {
        var fusedLocationClient = LocationServices.GetFusedLocationProviderClient(Application.Context);
        var location = await fusedLocationClient.LastLocation;

        if (location != null && location is Location deviceLocation)
        {
            string latitude = deviceLocation.Latitude.ToString();
            string longitude = deviceLocation.Longitude.ToString();

            // Generate Google Maps link
            string mapsLink = $"https://www.google.com/maps?q={latitude},{longitude}";

            // Return location details with the link
            return $"Latitude: {latitude}\nLongitude: {longitude}\nGoogle Maps Link: {mapsLink}";
        }
        else
        {
            // If location is null, return a fallback message
            return "Location unavailable";
        }
    }
}
