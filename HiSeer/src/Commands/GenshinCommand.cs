using System.Windows.Controls;
using HiSeer.src.Genshin;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Media;
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

        public override void ExecuteCommand(ListBox chatBox, string parameter)
        {
            switch (name)
            {
                case "character":
                    GetCharacterInfo(chatBox, parameter);
                    break;
                case "weapon":
                    GetWeaponInfo(chatBox, parameter);
                    break;
            }
        }
        
        void GetCharacterInfo(ListBox chatBox, string name)
        {
            string characterName = name.ToLower();
            var webRequest = WebRequest.Create("https://api.genshin.dev/characters/" + characterName) as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    Character character = JsonConvert.DeserializeObject<Character>(contributorsAsJson);
                    string characterInfo =
                        $"Name: {character.Name}\n" +
                        $"Vision: {character.Vision}\n" +
                        $"Weapon: {character.Weapon}\n" +
                        $"Nation: {character.Nation}\n" +
                        $"Affiliation: {character.Affiliation}\n" +
                        $"Rarity: {character.Rarity}\n" +
                        $"Birthday: {character.Birthday}\n";

                    chatBox.Items.Add(CreateImage($"https://api.genshin.dev/characters/{characterName}/card"));
                    chatBox.Items.Add(characterInfo);
                }
            }
        }

        void GetWeaponInfo(ListBox chatBox, string name)
        {
            string weaponName = name.ToLower();
            var webRequest = WebRequest.Create("https://api.genshin.dev/weapons/" + weaponName) as HttpWebRequest;
            if (webRequest == null)
            {
                return;
            }

            webRequest.ContentType = "application/json";
            webRequest.UserAgent = "Nothing";

            using (var s = webRequest.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var contributorsAsJson = sr.ReadToEnd();
                    Weapon weapon = JsonConvert.DeserializeObject<Weapon>(contributorsAsJson);

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
            }
        }

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
