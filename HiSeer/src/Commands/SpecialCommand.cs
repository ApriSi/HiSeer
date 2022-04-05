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

        public override void ExecuteCommand(ListView chatBox)
        {
            switch(name)
            {
                case "clear":
                    Clear(chatBox);
                    break;
            }
        }

        public override void ExecuteCommand(ListView chatBox, string parameter)
        {
            throw new NotImplementedException();
        }

        void Clear(ListView chatBox)
        {
            chatBox.Items.Clear();
        }
    }
}
