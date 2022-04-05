namespace HiSeer.src.Genshin
{
    public class Character
    {
        public string Name;
        public string Vision;
        public string Weapon;
        public string Nation;
        public string Affiliation;

        public int Rarity;

        public string Birthday;
        public string Description;

        public Character(string name, string vision, string weapon, string nation, string affiliation, int rarity, string birthday, string description)
        {
            Name = name;
            Vision = vision;
            Weapon = weapon;
            Nation = nation;
            Affiliation = affiliation;
            Rarity = rarity;
            Birthday = birthday;
            Description = description;
        }
    }
}
