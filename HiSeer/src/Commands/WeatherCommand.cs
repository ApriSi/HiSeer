using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public override void ExecuteCommand(ListBox chatBox, string[] parameter)
        {
            GetWeatherInfo(chatBox, parameter);
        }

        void GetWeatherInfo(ListBox chatBox, string[] parameter)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("X-RapidAPI-Host", "weather-by-api-ninjas.p.rapidapi.com");
            headers.Add("X-RapidAPI-Key", "33e7e23e6cmshc9258f82ac27d1bp15c44ajsn68058433470f");
            string json = WebsiteRequest.GetWebJson("https://weather-by-api-ninjas.p.rapidapi.com/v1/weather?city="+parameter[1].ToLower(), headers);

            Weather weather = JsonConvert.DeserializeObject<Weather>(json);

            string weatherInfo =
                $"Cloud: {weather.Cloud_pct}%\n" +
                $"Temperature: {weather.Temp}°C\n" +
                $"Feels like: {weather.Feels_like}°C\n" +
                $"Humidity: {weather.Humidity}%\n" +
                $"Min temperature: {weather.Min_temp}°C\n" +
                $"Max temperature: {weather.Max_temp}°C\n" +
                $"Wind speed: {weather.Wind_speed}M/s\n" +
                $"Wind  degrees: {weather.Wind_degrees}°";

            chatBox.Items.Add(weatherInfo);
        }
    }
}
