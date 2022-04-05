using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiSeer.src.Genshin
{
    public class Weapon
    {
        public string Name;
        public string Type;
        public int Rarity;
        public int BaseAttack;
        public string SubStat;
        public string PassiveName;
        public string PassiveDesc;
        public string Location;

        public Weapon(string name, string type, int rarity, int baseAttack, string subStat, string passiveName, string passiveDesc, string location)
        {
            Name = name;
            Type = type;
            Rarity = rarity;
            BaseAttack = baseAttack;
            SubStat = subStat;
            PassiveName = passiveName;
            PassiveDesc = passiveDesc;
            Location = location;
        }
    }
}
