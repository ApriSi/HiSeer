using System.Windows.Controls;
using HiSeer.src.Genshin;
using HiSeer.src;
using HiSeer.src.UserControls;
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
            CharacterControl characterControl = new CharacterControl();

            characterControl.NameLable.Text = character.Name;
            characterControl.RarityLable.Text = character.Rarity;
            characterControl.BirthdayLable.Text = character.Birthday;
            characterControl.WeaponLable.Text = character.Weapon;
            characterControl.AffiliationLable.Text = character.Affiliation;
            characterControl.VisionLable.Text = character.Vision;
            characterControl.NationLable.Text = character.Nation;
            characterControl.DescriptionLable.Text = character.Description;

            Image image = CreateImage($"https://api.genshin.dev/characters/{characterName}/card");

            characterControl.CharacterImage.Source = image.Source;
            chatBox.Items.Add(characterControl);
        }

        void GetWeaponInfo(ListBox chatBox, string name)
        {
            string weaponName = name.ToLower();

            string json = WebsiteRequest.GetWebJson("https://api.genshin.dev/weapons/" + weaponName, null);
            Weapon weapon = JsonConvert.DeserializeObject<Weapon>(json);
            WeaponControl weaponControl = new WeaponControl();
            weaponControl.NameLable.Text = weaponName;
            weaponControl.RarityLable.Text = weapon.Rarity;
            weaponControl.BaseAtkLable.Text = weapon.BaseAttack.ToString();
            weaponControl.TypeLable.Text = weapon.Type;
            weaponControl.SubstatLable.Text = weapon.SubStat;
            weaponControl.PassiveLable.Text = weapon.PassiveName;
            weaponControl.LocationLable.Text = weapon.Location;
            weaponControl.DescriptionLable.Text = weapon.PassiveDesc;

            Image image = CreateImage($"https://api.genshin.dev/weapons/{weaponName}/icon");

            weaponControl.WeaponIcon.Source = image.Source;
            chatBox.Items.Add(weaponControl);
        }


        // [TODO] Should probably be moved somewhere else [name] imagehandler.dk
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
