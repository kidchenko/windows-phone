using System;
using System.Collections.ObjectModel;

namespace BindingWithValueConverters
{
    public class DayForecast
    {
        public DateTime Date { get; set; }
        public Weather Weather { get; set; }
        public ObservableCollection<TimeForecast> HourlyForecast { get; set; }
    }

    public class TimeForecast
    {
        public int Hour { get; set; }
        public int Temperature { get; set; }
    }

    public enum Weather
    {
        Sunny,
        Cloudy,
        Rainy,
        Snowy
    }

    public class FiveDayForecast
    {
        public static ObservableCollection<DayForecast> GetForecast()
        {
            var forecast = new ObservableCollection<DayForecast>
      {
          new DayForecast
          {
              Date = new DateTime(2015, 12, 7),
              Weather = Weather.Rainy,
              HourlyForecast = GenerateRandomTimeForecast(7)
          },
          new DayForecast
          {
              Date = new DateTime(2015, 12, 8),
              Weather = Weather.Cloudy,
              HourlyForecast = GenerateRandomTimeForecast(8)
          },
          new DayForecast
          {
              Date = new DateTime(2015, 12, 9),
              Weather = Weather.Sunny,
              HourlyForecast = GenerateRandomTimeForecast(9)
          },
          new DayForecast
          {
              Date = new DateTime(2015, 12, 10),
              Weather = Weather.Snowy,
              HourlyForecast = GenerateRandomTimeForecast(10)
          }
      };

            return forecast;
        }

        private static ObservableCollection<TimeForecast> GenerateRandomTimeForecast(int seed)
        {
            var random = new Random(seed);
            var forecast = new ObservableCollection<TimeForecast>();

            for (var i = 0; i < 24; i++)
            {
                var timeForecast = new TimeForecast { Hour = i, Temperature = random.Next(105) };
                forecast.Add(timeForecast);
            }
            return forecast;
        }
    }
}
