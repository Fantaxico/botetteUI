﻿using BotetteUI.IO;
using BotetteUI.Models.Stucts;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BotetteUI.Models
{
    public class Data : Jsonable<Data>
    {
        public List<string> Pokemon { get; set; }
        public List<string> Moves { get; set; }
        public List<string> Directions { get; set; }
        public List<Pokeball> Pokeballs { get; set; }

        [JsonConstructor]
        public Data(List<string> pokemon, List<string> moves, List<string> directions, List<Pokeball> balls)
        {
            this.Pokemon = pokemon;
            this.Moves = moves;
            this.Directions = directions;
            this.Pokeballs = balls;
        }

        public Data()
        {
            this.Pokemon = new List<string>();
            this.Moves = new List<string>();
            this.Directions = new List<string>();
            this.Pokeballs = new List<Pokeball>();
        }
    }
}
