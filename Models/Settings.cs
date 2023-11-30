using BotetteUI.IO;
using Newtonsoft.Json;

namespace BotetteUI.Models
{
    public class Settings : Jsonable<Settings>
    {
        public int FirstStartup { get; set; }
        public string WorkingDirectory { get; set; }
        public string PBOPath { get; set; }

        [JsonConstructor]
        public Settings(int firstStart,string workingDirectory, string pBOPath)
        {
            FirstStartup = firstStart;
            WorkingDirectory = workingDirectory;
            PBOPath = pBOPath;
        }

        public Settings()
        {
            FirstStartup = 1;
            WorkingDirectory = string.Empty;
            PBOPath = string.Empty;
        }
    }
}
