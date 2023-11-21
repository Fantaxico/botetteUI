using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotetteUI.Models
{
    public class Target
    {
        public string Name { get; set; }

        public Pokeball PriorityBall { get; set; }

        public Target(string name, Pokeball priorityBall)
        {
            Name = name;
            PriorityBall = priorityBall;
        }
    }
}
