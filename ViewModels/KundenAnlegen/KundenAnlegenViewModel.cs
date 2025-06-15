
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
        private readonly IDialogService _dialogService;

        public KundenAnlegenViewModel(INavigationService navigationService, IDatabaseService databaseService,IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
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
                await _dialogService.DisplayAlert("Fehler", "Bitte geben Sie sowohl den Kundennamen als auch die Adresse ein.", "OK");
                return;
            }
            var kunde = new Kunde
            {
                KundenName = this.Kundenname,
                KundenAdresse = this.Kundenadresse
            };
            var kundeCheck = await _databaseService.GetKundeByNameAsync(Kundenname);

            if (kundeCheck != null)
            {
                await _dialogService.DisplayAlert("Fehler", "Kunde bereits vorhanden", "OK");
                return;
            }
            int result = await _databaseService.SaveKundeAsync(kunde);

            if (result != 0)
            {
                await _dialogService.DisplayAlert("Erfolg", "Kunde erfolgreich gespeichert.", "OK");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _dialogService.DisplayAlert("Fehler", "Fehler beim Speichern des Kunden.", "OK");

            }
        }
    }
}
