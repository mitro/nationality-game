using System.IO;
using NationalityGame.Mechanics.Configuration;
using Newtonsoft.Json;

namespace NationalityGame.App
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