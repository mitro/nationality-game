using System.IO;
using NationalityGame.Mechanics.Configuration;
using Newtonsoft.Json;

namespace NationalityGame.App
{
    public class SettingsReader
    {
        public GameSettings Read()
        {
            var settingsText = File.ReadAllText(".\\settings.json");

            return JsonConvert.DeserializeObject<GameSettings>(settingsText);
        }
    }
}