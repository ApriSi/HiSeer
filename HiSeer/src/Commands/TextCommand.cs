using System.Windows.Controls;
using System.Windows.Media;

namespace HiSeer.src.Commands
{
    public class TextCommand : Command
    {
        string output;
        public TextCommand(string commandName, string commandUsage, ItemsControl chatBox, string output) : base(commandName, commandUsage, chatBox)
        {
            this.output = output;
        }

        public override void ExecuteCommand()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = output;
            textBlock.Foreground = Brushes.White;
            ChatBox.Items.Add(textBlock);
        }

        public override void ExecuteCommand(string[] parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
