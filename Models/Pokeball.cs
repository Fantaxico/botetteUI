using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotetteUI.Models
{
    public enum POKEBALLS : int
    {
        MASTER_BALL = 0,
        ULTRA_BALL = 1,
        GREAT_BALL = 2
    }
    public struct Pokeball
    {
        public string Name { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; }

        [JsonConstructor]
        public Pokeball(string name)
        {
            this.Name = name;
            this.ImagePath = $"Assets/{name.ToLower()}.png";
        }
        public Pokeball(string name, string imagepath)
        {
            this.Name = name;
            this.ImagePath = imagepath;
        }

    }
}
