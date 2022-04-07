using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using HiSeer.src.UserControls;

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

            WeatherControl weatherControl = new WeatherControl();

            if (weather.Cloud_pct > 45)
                weatherControl.WeatherLabel.Text = "☁️";
            else if (weather.Cloud_pct > 15)
                weatherControl.WeatherLabel.Text = "⛅";
            else
                weatherControl.WeatherLabel.Text = "☀️";

            weatherControl.LocationLabel.Text = parameter[1].ToUpper();
            weatherControl.TemperatureLabel.Text = "Temperature: " + weather.Temp.ToString() + "°C";
            weatherControl.FeelsLikeLabel.Text = "Feels Like: " + weather.Feels_like.ToString() + "°C";
            weatherControl.MinTemperatureLabel.Text = "Min Temperature: " + weather.Min_temp.ToString() + "°C";
            weatherControl.MaxTemperatureLabel.Text = "Max Temperature: " + weather.Max_temp.ToString() + "°C";
            weatherControl.CloudPercentageLabel.Text = "Cloud Coverage: " + weather.Cloud_pct.ToString() + "%";
            weatherControl.WindSpeedLabel.Text = "Wind Speed: " + weather.Wind_speed.ToString() + "m/s";
            weatherControl.WindDegreesLabel.Text = "Wind Degrees: " + weather.Wind_degrees.ToString() + "°";
            weatherControl.HumidityLabel.Text = "Humidity: " + weather.Humidity.ToString();

            chatBox.Items.Add(weatherControl);
        }
    }
}
