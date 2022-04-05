using HiSeer.src.Commands;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace HiSeer
{
    public partial class MainWindow : Window
    {
        List<Command> commands = new List<Command>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();

        private void OnExitClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void OnLoadedChatBox(object sender, RoutedEventArgs e)
        {
            commands.Add(new ImageCommand("image", "/image ImageName"));
            commands.Add(new SpecialCommand("clear", "/clear"));
            LoadTextCommands();
        }

        private void InputGetKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                foreach (Command command in commands)
                {
                    string[] commandName = CommandInput.Text.Split(' ');
                    if (commandName[0] == "/" + command.GetName())
                    {
                        try
                        {
                            if (commandName.Length > 1 && commandName[1] != null)
                                command.ExecuteCommand(ChatBox, commandName[1]);
                            else
                                command.ExecuteCommand(ChatBox);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            CommandInput.Text = "this command isn't valid!";
                        }
                    }
                }
            }
        }
        
        private void ExecuteImageCommand(ImageCommand command, string[] commandName)
        {
            try
            {
                //command.ImagePath = commandName[1];
                command.ExecuteCommand(ChatBox);
            }
            catch
            {
                CommandInput.Text = "Please write /image imageurl";
            }
        }

        private void LoadTextCommands()
        {
            string commandList = "/help\n";

            string fileText = File.ReadAllText($@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}/src/Commands/Commands.json");
            JObject command = JObject.Parse(fileText);

            int length = ((JObject)command["TextCommands"]).Count;
            for (int i = 0; i < length; i++)
            {
                TextCommand textCommand = JsonConvert.DeserializeObject<TextCommand>(command["TextCommands"][i.ToString()].ToString());
                commands.Add(textCommand);
            }

            foreach (Command cmd in commands)
            {
                commandList += cmd.GetUsage() + "\n";
            }
            commands.Add(new TextCommand("help", "/help", commandList));
        }
    }
}
