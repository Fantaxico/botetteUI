using BotetteUI.IO;
using BotetteUI.Models.Stucts;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public Notifications UserNotifications { get; set; }
        public string DiscordUserId { get; set; }
        public bool Debug { get; set; }
        public int TickChatter { get; set; }
        public int TickWatcher { get; set; }

        [JsonConstructor]
        public Config(
            List<Target> targets,
            string move,
            string runningDirection,
            bool invertRunning,
            int runningRandomness,
            bool hunt,
            bool fleeFromFights,
            Notifications notifications,
            string discordId,
            bool debug,
            int tickChatter,
            int tickWatcher)
        {
            Targets = targets;
            MoveToUse = move;
            RunningDirection = runningDirection;
            RunningInvert = invertRunning;
            RunningRandomness = runningRandomness;
            HuntingMode = hunt;
            FleeFromFights = fleeFromFights;
            UserNotifications = notifications;
            DiscordUserId = discordId;
            Debug = debug;
            TickChatter = tickChatter;
            TickWatcher = tickWatcher;
        }

        public Config()
        {
            Targets = new List<Target>();
            MoveToUse = "1";
            RunningDirection = "Left/Right";
            RunningInvert = false;
            RunningRandomness = 3;
            HuntingMode = true;
            FleeFromFights = false;
            UserNotifications = new Notifications();
            DiscordUserId = string.Empty;
            Debug = false;
            TickChatter = 10;
            TickWatcher = 6;
        }
    }
}
