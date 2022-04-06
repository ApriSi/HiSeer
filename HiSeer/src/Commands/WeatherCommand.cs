using System;
using System.Windows.Controls;

namespace HiSeer.src.Commands
{
    public class WeatherCommand : Command
    {
        public WeatherCommand(string commandName, string commandUsage) : base(commandName, commandUsage)
        {
        }

        public override void ExecuteCommand(ListBox chatBox)
        {
            throw new NotImplementedException();
        }

        public override void ExecuteCommand(ListBox chatBox, string parameter)
        {
            throw new NotImplementedException();
        }
    }
}
