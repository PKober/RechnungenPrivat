using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Data.Services;


namespace RechnungenPrivat.ViewModels.KundenLöschen
{
    public partial class KundenLöschenViewModel : ObservableObject
    {

        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        public KundenLöschenViewModel(INavigationService navigationService, IDatabaseService databaseService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
        }

        [ObservableProperty]
        private string? _kundenname;

        [ObservableProperty]
        private string? _kundenadresse;



        [RelayCommand]
        public async Task LöscheKundeAsync()
        {
            if (string.IsNullOrWhiteSpace(Kundenname))
            {
                await _dialogService.DisplayAlert("Fehler", "Bitte geben Sie sowohl den Kundennamen als auch die Adresse ein.", "OK");
                return;
            }

            var kundeCheck = await _databaseService.GetKundeByNameAsync(Kundenname);

            if (kundeCheck == null)
            {
                await _dialogService.DisplayAlert("Fehler", "Kunde nicht gefunden.", "OK");
                return;
            }
            int result = await _databaseService.DeleteKundeAsync(kundeCheck);
            if (result != 0)
            {
                await _dialogService.DisplayAlert("Erfolg", "Kunde erfolgreich gelöscht.", "OK");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _dialogService.DisplayAlert("Fehler", "Fehler beim Löschen des Kunden.", "OK");

            }
        }
    }
}
