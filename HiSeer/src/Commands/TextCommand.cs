using System.Windows.Controls;
using System.Windows.Media;

namespace HiSeer.src.Commands
{
    public class TextCommand : Command
    {
        string output;
        public TextCommand(string commandName, string commandUsage, string output) : base(commandName, commandUsage)
        {
            this.output = output;
        }

        public override void ExecuteCommand(ListBox chatBox)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = output;
            textBlock.Foreground = Brushes.White;
            chatBox.Items.Add(textBlock);
        }

        public override void ExecuteCommand(ListBox chatBox, string parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
