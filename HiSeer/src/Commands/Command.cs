using System.Windows.Controls;

namespace HiSeer.src.Commands
{
    public abstract class Command
    {
        public ItemsControl ChatBox;
        string commandName;
        string commandUsage;

        public Command(string commandName, string commandUsage, ItemsControl chatBox)
        {
            this.commandName = commandName;
            this.commandUsage = commandUsage;
            this.ChatBox = chatBox;
        }

        public abstract void ExecuteCommand();
        public abstract void ExecuteCommand(string[] parameter);

        public string GetUsage() => commandUsage;
        public string GetName() => commandName;
    }
}
