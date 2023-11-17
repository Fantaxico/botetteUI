using BotetteUI.Helper;
using BotetteUI.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BotetteUI.Models
{
    public class Config : Jsonable<Config>
    {
        public List<string> Targets { get; set; }
        public string Move { get; set; }
        public bool Hunt { get; set; }
        public bool RunFromFights { get; set; }
        public bool Debug { get; set; }

        [JsonConstructor]
        public Config(List<string> targets, string move, bool hunt, bool runFromFights, bool debug)
        {
            Targets = targets;
            Move = move;
            Hunt = hunt;
            RunFromFights = runFromFights;
            Debug = debug;
        }

        public Config(string wd)
        {
            Targets = new List<string>();
            Move = string.Empty;
            Hunt = true;
            RunFromFights = false;
            Debug = false;
        }
    }
}
