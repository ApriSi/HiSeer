using System.Windows.Controls;

namespace HiSeer.src.Commands
{
    public abstract class Command
    {
        string commandName;
        string commandUsage;

        public Command(string commandName, string commandUsage)
        {
            this.commandName = commandName;
            this.commandUsage = commandUsage;
        }

        public abstract void ExecuteCommand(ListView chatBox);

        public string GetUsage() => commandUsage;
        public string GetName() => commandName;
    }
}
