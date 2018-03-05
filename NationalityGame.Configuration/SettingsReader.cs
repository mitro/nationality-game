using System.IO;
using NationalityGame.Configuration.Settings;
using Newtonsoft.Json;

namespace NationalityGame.Configuration
{
    public class SettingsReader
    {
        private const string SettingsFilePath = ".\\settings.json";

        public GameSettings Read()
        {
            var settingsText = File.ReadAllText(SettingsFilePath);

            return JsonConvert.DeserializeObject<GameSettings>(settingsText);
        }
    }
}