using BotetteUI.Models.Stucts;

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
