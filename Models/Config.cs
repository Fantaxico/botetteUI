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
        public List<Target> Targets { get; set; }
        public string MoveToUse { get; set; }
        public string RunningDirection { get; set; }
        public bool RunningInvert { get; set; }
        public int RunningRandomness { get; set; }
        public bool HuntingMode { get; set; }
        public bool FleeFromFights { get; set; }
        public bool Debug { get; set; }

        [JsonConstructor]
        public Config(List<Target> targets, string move, string runningDirection, bool invertRunning, int runningRandomness, bool hunt, bool fleeFromFights, bool debug)
        {
            Targets = targets;
            MoveToUse = move;
            RunningDirection = runningDirection;
            RunningInvert = invertRunning;
            RunningRandomness = runningRandomness;
            HuntingMode = hunt;
            FleeFromFights = fleeFromFights;
            Debug = debug;
        }

        public Config()
        {
            Targets = new List<Target>();
            MoveToUse = string.Empty;
            RunningDirection = "Left/Right";
            RunningInvert = false;
            RunningRandomness = 3;
            HuntingMode = true;
            FleeFromFights = false;
            Debug = false;
        }
    }
}
