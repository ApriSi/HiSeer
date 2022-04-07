using HiSeer.src.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HiSeer.src.Commands
{
    public class SpecialCommand : Command
    {
        public SpecialCommand(string commandName, string commandUsage, ItemsControl chatBox) : base(commandName, commandUsage, chatBox)
        {
        }

        public override void ExecuteCommand()
        {
            switch (GetName())
            {
                case "clear":
                    Clear();
                    break;
                case "dog":
                    Dog(null);
                    break;
                case "help":
                    HelpCommand(null);
                    break;
            }
        }

        public override void ExecuteCommand(string[] parameter)
        {
            switch (GetName())
            {
                case "reply":
                    Reply(parameter);
                    break;
                case "dog":
                    Dog(parameter);
                    break;
                case "help":
                    HelpCommand(parameter);
                    break;
            }
        }

        void Clear()
        {
            ChatBox.Items.Clear();
        }

        void HelpCommand(string[] parameter)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            string commandList = string.Empty;
            if(parameter != null)
            {
                foreach (var command in window.GetCommands())
                {
                    if (command.GetType().Name.ToLower() == parameter[1].ToLower())
                        commandList += command.GetUsage() + "\n";
                }
            } else
            {
                foreach (var command in window.GetCommands())
                {
                    commandList += command.GetUsage() + "\n";
                }
            }

            TextBlock textBlock = new TextBlock();
            textBlock.Foreground = Brushes.White;
            textBlock.Text = commandList;
            textBlock.Margin = new Thickness(3,0,0,0);

            ChatBox.Items.Add(textBlock);
        }

        void Reply(string[] parameter)
        {
            string text = "";
            for (int i = 1; i < parameter.Length; i++)
            {
                text += " " + parameter[i];
            }
            ChatBox.Items.Add(text);
        }

        void Dog(string[] parameter)
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

            ChatBox.Items.Add(dogControl);
        }
    }
}
