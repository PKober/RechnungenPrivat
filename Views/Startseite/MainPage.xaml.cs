using RechnungenPrivat.Services;
using RechnungenPrivat.ViewModels.Startseite;

namespace RechnungenPrivat.Views.Startseite
{


    public partial class MainPage : ContentPage
    {
        private readonly WeatherService _weatherService;
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = mainPageViewModel;
            _weatherService = new WeatherService();
        }

        private async void OnGetWeatherClicked(object sender, EventArgs e)
        {

                var weatherData = await _weatherService.GetWeatherForCityAsync(); // Annahme: diese Methode gibt jetzt WeatherApiResponse zurück
                if (weatherData != null)
                {
                    // Zugriff über die neuen Eigenschaften
                    TempLabel.Text = $"Temperatur: {weatherData.Current.Temperature}{weatherData.Main.Temperature}"; // Ergibt z.B. "14.6°C"
                    DescLabel.Text = $"Regen: {weatherData.Current.Rain} {weatherData.Main.Rain}"; // Ergibt z.B. "0.00 mm"

                    // Diese API liefert kein Icon, also müsstest du das Image ausblenden oder basierend auf den Daten (z.B. Regen > 0) selbst eines setzen.
                    WeatherIcon.IsVisible = false;
                }
                else
                {
                    await DisplayAlert("Fehler", "Wetterdaten konnten nicht abgerufen werden.", "OK");
                }
            
        }

    }

}
