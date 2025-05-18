
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;


namespace RechnungenPrivat.ViewModels.KundenAnlegen
{
    public partial class KundenAnlegenViewModel : ObservableObject
    {
        
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;

        public KundenAnlegenViewModel(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
        }

        [ObservableProperty]
        private string? _kundenname;

        [ObservableProperty]
        private string? _kundenadresse;


        //Comands
        [RelayCommand]
        public async Task SaveKundeAsync()
        {
            if (string.IsNullOrWhiteSpace(Kundenname) || string.IsNullOrWhiteSpace(Kundenadresse))
            {
                await Shell.Current.DisplayAlert("Fehler", "Bitte geben Sie sowohl den Kundennamen als auch die Adresse ein.", "OK");
                return;
            }
            var kunde = new Kunde
            {
                Kundenname = this.Kundenname,
                KundenAdresse = this.Kundenadresse
            };
            int result = await _databaseService.SaveKundeAsync(kunde);

            if (result != 0)
            {
                await Shell.Current.DisplayAlert("Erfolg", "Kunde erfolgreich gespeichert.", "OK");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Fehler", "Fehler beim Speichern des Kunden.", "OK");
               
            }
        }
    }
}
