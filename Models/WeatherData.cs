using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RechnungenPrivat.Models
{
    public class WeatherData
    {


        [JsonPropertyName("current_units")]
        public CurrentUnits Main { get; set; }

        [JsonPropertyName("current")]
        public CurrentWeather Current { get; set; }
    }

    public class CurrentUnits
    {
        [JsonPropertyName("temperature_2m")]
        public string Temperature { get; set; }
        [JsonPropertyName("rain")]
        public string Rain { get; set; }
    }

    public class CurrentWeather
    {
        
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("rain")]
        public double Rain { get; set; }
    }
}
