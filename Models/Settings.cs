using BotetteUI.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace BotetteUI.Models
{
    public class Settings : Jsonable<Settings>
    {
        public string WorkingDirectory { get; set; }
        public string PBOPath { get; set; }

        [JsonConstructor]
        public Settings(string workingDirectory, string pBOPath)
        {
            WorkingDirectory = workingDirectory;
            PBOPath = pBOPath;
        }

        public Settings(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            PBOPath = string.Empty;
        }
    }
}
