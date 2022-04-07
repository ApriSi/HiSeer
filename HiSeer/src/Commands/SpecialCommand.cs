using HiSeer.src.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Controls;

namespace HiSeer.src.Commands
{
    public class SpecialCommand : Command
    {
        string name;
        public SpecialCommand(string commandName, string commandUsage) : base(commandName, commandUsage)
        {
            name = commandName;
        }

        public override void ExecuteCommand(ListBox chatBox)
        {
            switch (name)
            {
                case "clear":
                    Clear(chatBox);
                    break;
                case "dog":
                    Dog(chatBox, null);
                    break;
            }
        }

        public override void ExecuteCommand(ListBox chatBox, string[] parameter)
        {
            switch (name)
            {
                case "reply":
                    Reply(chatBox, parameter);
                    break;
                case "dog":
                    Dog(chatBox, parameter);
                    break;
            }
        }

        void Clear(ListBox chatBox)
        {
            chatBox.Items.Clear();
        }

        void Reply(ListBox chatBox, string[] parameter)
        {
            string text = "";
            for (int i = 1; i < parameter.Length; i++)
            {
                text += " " + parameter[i];
            }
            chatBox.Items.Add(text);
        }

        void Dog(ListBox chatbox, string[] parameter)
        {
            string json;

            if (parameter != null)
            {
                string parm = parameter[1].ToLower().Replace('-', '/');
                json = WebsiteRequest.GetWebJson($"https://dog.ceo/api/breed/{parm}/images/random", null);
            }
            else
            {
                json = WebsiteRequest.GetWebJson($"https://dog.ceo/api/breeds/image/random", null);
            }

            var obj = JsonConvert.DeserializeObject(json) as JObject;
            string dog = obj["message"].ToString();

            Image image = ImageHandler.CreateImage(dog);
            string breed = dog.Substring(dog.LastIndexOf("/breeds/") + 8);

            int index = breed.LastIndexOf("/");

            if (breed.LastIndexOf("/") > 0)
                breed = breed.Substring(0, index);

            breed = breed.Replace("-", " ");

            DogControl dogControl = new DogControl();
            dogControl.BreedLable.Text = breed;
            dogControl.DogImage.Source = image.Source;

            chatbox.Items.Add(dogControl);
        }
    }
}
