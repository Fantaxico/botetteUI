using BotetteUI.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotetteUI.Models
{
    public class Settings : Jsonable<Settings>
    {
        public string WorkingDirectory { get; set; }
        public Settings(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }
    }
}
