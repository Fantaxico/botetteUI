using BotetteUI.Helper;
using BotetteUI.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps;

namespace BotetteUI.Models
{
    public class Data: Jsonable<Data>
    {
        public List<string> Pokemon { get; set; }
        public List<string> Moves { get; set; }
        public List<Pokeball> Pokeballs { get; set; }

        [JsonConstructor]
        public Data(List<string> pokemon, List<string> moves, List<Pokeball> balls)
        {
            this.Pokemon = pokemon;
            this.Moves = moves;
            this.Pokeballs = balls;
        }

        public Data()
        {
            this.Pokemon = new List<string>();
            this.Moves = new List<string>();
            this.Pokeballs = new List<Pokeball>();
        }
    }
}
