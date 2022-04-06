namespace HiSeer.src
{
    public class Weather { 
    
        public int Cloud_pct;
        public int Temp;
        public int Feels_like;
        public int Humidity;
        public int Min_temp;
        public int Max_temp;
        public float Wind_speed;
        public int Wind_degrees;

        public Weather(int cloud_pct, int temp, int feels_like, int humidity, int min_temp, int max_temp, float wind_speed, int wind_degrees)
        {
            Cloud_pct = cloud_pct;
            Temp = temp;
            Feels_like = feels_like;
            Humidity = humidity;
            Min_temp = min_temp;
            Max_temp = max_temp;
            Wind_speed = wind_speed;
            Wind_degrees = wind_degrees;
        }
    }
}
