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

        public MainWindow() => InitializeComponent();

        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();

        private void OnExitClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
        private void OnMinimizeClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void OnLoadedChatBox(object sender, RoutedEventArgs e)
        {
            commands.Add(new ImageCommand("image", "/image url"));

            LoadCommands<SpecialCommand>("SpecialCommands");
            LoadCommands<GenshinCommand>("GenshinCommands");
            LoadCommands<TextCommand>("TextCommands");
            HelpCommand();
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
                            CommandInput.Text = "This isn't a valid command!";
                        }
                    }
                }
            }
        }

        private void HelpCommand()
        {
            string commandList = "/help\n";
            foreach (Command cmd in commands)
            {
                commandList += cmd.GetUsage() + "\n";
            }
            commands.Add(new TextCommand("help", "/help", commandList));
        }

        private void LoadCommands<T>(string commandType)
        {
            JObject cmds = GetJsonObject($@"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}/src/Commands/Commands.json");
            int length = ((JObject)cmds[commandType]).Count;
            for (int i = 0; i < length; i++)
            {
                T command = JsonConvert.DeserializeObject<T>(cmds[commandType][i.ToString()].ToString());
                commands.Add(command as Command);
            }
        }

        private JObject GetJsonObject(string path)
        {
            string json = File.ReadAllText(path);
            var obj = JObject.Parse(json);

            return obj;
        }
    }
}
