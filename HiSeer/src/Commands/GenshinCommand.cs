using System.Windows.Controls;
using HiSeer.src.Genshin;
using HiSeer.src;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;

namespace HiSeer.src.Commands
{
    public class GenshinCommand : Command
    {
        string name;
        public GenshinCommand(string commandName, string commandUsage) : base(commandName, commandUsage)
        {
            name = commandName;
        }

        public override void ExecuteCommand(ListBox chatBox)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteCommand(ListBox chatBox, string[] parameter)
        {
            switch (name)
            {
                case "character":
                    GetCharacterInfo(chatBox, parameter[1]);
                    break;
                case "weapon":
                    GetWeaponInfo(chatBox, parameter[1]);
                    break;
            }
        }
        
        void GetCharacterInfo(ListBox chatBox, string name)
        {
            string characterName = name.ToLower();

            string json = WebsiteRequest.GetWebJson("https://api.genshin.dev/characters/" + characterName, null);
            Character character = JsonConvert.DeserializeObject<Character>(json);

            string characterInfo =
                $"Name: {character.Name}\n" +
                $"Vision: {character.Vision}\n" +
                $"Weapon: {character.Weapon}\n" +
                $"Nation: {character.Nation}\n" +
                $"Affiliation: {character.Affiliation}\n" +
                $"Rarity: {character.Rarity}\n" +
                $"Birthday: {character.Birthday}";

            chatBox.Items.Add(CreateImage($"https://api.genshin.dev/characters/{characterName}/card"));
            chatBox.Items.Add(characterInfo);
        }

        void GetWeaponInfo(ListBox chatBox, string name)
        {
            string weaponName = name.ToLower();

            string json = WebsiteRequest.GetWebJson("https://api.genshin.dev/weapons/" + weaponName, null);
            Weapon weapon = JsonConvert.DeserializeObject<Weapon>(json);

            string weaponInfo =
                $"Name: {weapon.Name}\n" +
                $"type: {weapon.Type}\n" +
                $"Rarity: {weapon.Rarity}\n" +
                $"Base Attack: {weapon.BaseAttack}\n" +
                $"Sub Stat: {weapon.SubStat}\n" +
                $"Passive Name: {weapon.PassiveName}\n";

            chatBox.Items.Add(CreateImage($"https://api.genshin.dev/weapons/{weaponName}/icon"));
            chatBox.Items.Add(weaponInfo);
        }


        // [TODO] Should probably be moved somewhere else
        Image CreateImage(string path)
        {
            Image icon = new Image();
            Uri uri = new Uri(path);
            icon.Source = new BitmapImage(uri);
            icon.Width = 100;

            return icon;
        }
    }
}
