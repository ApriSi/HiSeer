namespace HiSeer.src.Genshin
{
    public class Character
    {
        public string Name;
        public string Vision;
        public string Weapon;
        public string Nation;
        public string Affiliation;
        public string Description;

        public string Rarity;

        public string Birthday;

        public Character(string name, string vision, string weapon, string nation, string affiliation, string description, string rarity, string birthday)
        {
            Name = name;
            Vision = vision;
            Weapon = weapon;
            Nation = nation;
            Affiliation = affiliation;
            Description = description;
            for (int i = 0; i < int.Parse(rarity); i++)
            {
                Rarity += "★";
            }
            if(birthday != null)
            {
                Birthday = birthday.Replace("0000-", "");
            }

        }
    }
}
