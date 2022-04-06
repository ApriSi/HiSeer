using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                    Dog(chatBox);
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

        void Dog(ListBox chatbox)
        {
            string json = WebsiteRequest.GetWebJson($"https://dog.ceo/api/breeds/image/random", null);
            var obj = JsonConvert.DeserializeObject(json) as JObject;
            Image image = ImageHandler.CreateImage(obj["message"].ToString());
            chatbox.Items.Add(image);
        }
    }
}
