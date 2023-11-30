using Newtonsoft.Json;

namespace BotetteUI.Models.Stucts
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
            Name = name;
            ImagePath = $"Assets/{name.ToLower()}.png";
        }
        public Pokeball(string name, string imagepath)
        {
            Name = name;
            ImagePath = imagepath;
        }

    }
}
