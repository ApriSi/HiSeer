using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HiSeer.src.Commands
{
    public class ImageCommand : Command
    {
        public string ImagePath;

        public ImageCommand(string commandName, string commandUsage) : base(commandName, commandUsage)
        {
        }

        public override void ExecuteCommand(ListView chatBox)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(ImagePath));
    
            image.Width = 100;
            chatBox.Items.Add(image);
        }
    }
}
