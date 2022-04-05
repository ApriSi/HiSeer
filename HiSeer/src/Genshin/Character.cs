namespace HiSeer.src.Genshin
{
    public class Character
    {
        public string Name;
        public string Vision;
        public string Weapon;
        public string Nation;
        public string Affiliation;

        public string Rarity;

        public string Birthday;
        public string Description;

        public Character(string name, string vision, string weapon, string nation, string affiliation, string rarity, string birthday, string description)
        {
            Name = name;
            Vision = vision;
            Weapon = weapon;
            Nation = nation;
            Affiliation = affiliation;
            for (int i = 0; i < int.Parse(rarity); i++)
            {
                Rarity += "★";
            }
            Birthday = birthday.Replace("0000-", "");
            Description = description;
        }
    }
}
