using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RechnungenPrivat.Services
{
    class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherForCityAsync()
        {
            var uri = $"https://api.open-meteo.com/v1/forecast?latitude=51.2513&longitude=6.9786&current=temperature_2m,rain";
            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherData>(jsonResponse);
            }

            return null;
        }
    }
}
