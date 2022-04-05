using System.Windows.Controls;
using HiSeer.src.Genshin;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Media;

namespace HiSeer.src.Commands
{
    public class SpecialCommand : Command
    {
        string name;
        public SpecialCommand(string commandName, string commandUsage) : base(commandName, commandUsage)
        {
            name = commandName;
        }

        public override void ExecuteCommand(ListView chatBox)
        {
            switch (name)
            {
                case "clear":
                    Clear(chatBox);
                    GetWeaponInfo(chatBox, "alley-hunter");
                    break;
            }
        }

        public override void ExecuteCommand(ListView chatBox, string parameter)
        {
            switch (name)
            {
                case "character":
                    GetCharacterInfo(chatBox, parameter);
                    break;
            }
        }

        void Clear(ListView chatBox)
        {
            chatBox.Items.Clear();
        }

        
        void GetCharacterInfo(ListView chatBox, string characterName)
        {
            //string characterInfo = File.ReadAllText(new Uri("https://api.genshin.dev/characters/albedo").ToString());

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
                        $"Birthday: {character.Birthday}\n" +
                        $"Description: {character.Description}";
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = characterInfo;
                    textBlock.Width = 200;
                    chatBox.Items.Add(textBlock);
                }
            }
            //Console.WriteLine(characterInfo);
        }


        void GetWeaponInfo(ListView chatBox, string weaponName)
        {
            //string characterInfo = File.ReadAllText(new Uri("https://api.genshin.dev/characters/albedo").ToString());

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
                    Console.WriteLine(weapon);
                    string weaponInfo =
                        $"Name: {weapon.Name}\n" +
                        $"type: {weapon.Type}\n" +
                        $"Rarity: {weapon.Rarity}\n" +
                        $"Base Attack: {weapon.BaseAttack}\n" +
                        $"Sub Stat: {weapon.SubStat}\n" +
                        $"Passive Name: {weapon.PassiveName}\n" +
                        $"Description:\n{weapon.PassiveDesc}";

                    chatBox.Items.Add(weaponInfo);
                }
            }
            //Console.WriteLine(characterInfo);
        }
    }
}
