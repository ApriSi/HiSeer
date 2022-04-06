using System.Windows.Controls;
using System;

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
            }
        }

        public override void ExecuteCommand(ListBox chatBox, string[] parameter)
        {
            switch (name)
            {
                case "reply":
                    Reply(chatBox, parameter[1]);
                    break;
            }
        }

        void Clear(ListBox chatBox)
        {
            chatBox.Items.Clear();
        }

        void Reply(ListBox chatBox, string parameter)
        {
            chatBox.Items.Add(parameter);
        }
    }
}
