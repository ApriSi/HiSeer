using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HiSeer.src.Commands
{
    public class ImageCommand : Command
    {
        public ImageCommand(string commandName, string commandUsage, ItemsControl chatBox) : base(commandName, commandUsage, chatBox)
        {
        }

        public override void ExecuteCommand()
        {
        }

        public override void ExecuteCommand(string[] parameter)
        {
            Image image = new Image();
            try
            {
                Uri uri = new Uri(parameter[1]);
                image.Source = new BitmapImage(uri);

                image.Width = 280;
                ChatBox.Items.Add(image);
            } catch
            {
                Console.WriteLine("Not valid image");
            }
        }
    }
}
