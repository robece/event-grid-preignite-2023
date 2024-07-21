using System.Text.Json;

namespace Namespace.PushPull
{
    internal class Utils
    {
        public static Settings? GetSettings()
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Namespace.PushPull.settings.json");
                string strSettings = File.ReadAllText(filePath);
                if (string.IsNullOrEmpty(strSettings))
                    return null;

                return JsonSerializer.Deserialize<Settings>(strSettings);
            }
            catch
            {
                return null;
            }
        }
    }
}
