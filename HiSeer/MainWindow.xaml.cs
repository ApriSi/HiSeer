using HiSeer.src.Commands;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using HiSeer.src;

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
            commands.Add(new ImageCommand("image", "/image url", ChatBox));

            LoadCommandsFromJson<SpecialCommand>("SpecialCommands");
            LoadCommandsFromJson<GenshinCommand>("GenshinCommands");
            LoadCommandsFromJson<TextCommand>("TextCommands");
            LoadCommandsFromJson<WeatherCommand>("WeatherCommands");
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
                                command.ExecuteCommand(commandName);
                            else
                                command.ExecuteCommand();
                            CommandInput.Text = "";
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            CommandInput.Text = "This isn't a valid command!";
                        }
                    }
                }
            }
        }

        private void LoadCommandsFromJson<T>(string commandType)
        {
            JObject cmds = JsonHandler.GetJsonObject($@"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}/src/Commands/Commands.json");
            int length = ((JObject)cmds[commandType]).Count;
            for (int i = 0; i < length; i++)
            {
                T command = JsonConvert.DeserializeObject<T>(cmds[commandType][i.ToString()].ToString());
                Command cmd = command as Command;
                cmd.ChatBox = ChatBox;
                commands.Add(cmd);
            }
        }

        public List<Command> GetCommands() => commands;

        private void CommandInputGetMouseCapture(object sender, MouseEventArgs e) => CommandInput.Text = "";
    }
}
