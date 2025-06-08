using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.Startseite;

namespace RechnungenPrivat.ViewModels.AuftragErstellen
{
    [QueryProperty(nameof(KundenId), "KundenId")]
    public partial class AuftragErstellenViewModel : ObservableObject
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public AuftragErstellenViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;


        }

        [ObservableProperty]
        private int _kundenId;

        [ObservableProperty]
        private string _auftragsname;

        [ObservableProperty]
        private string _auftragsbeschreibung;

        [ObservableProperty]
        private DateOnly _auftragsdatum;

        [ObservableProperty]
        private decimal _stunden;

        [ObservableProperty]
        private decimal _stundensatz;
        [ObservableProperty]
        private decimal _betrag;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsStundenbasiert))] // Benachrichtigt UI, wenn sich Sichtbarkeit ändert
        private Auftragstyp _selectedAuftragstyp = Auftragstyp.Pauschal;

        public List<Auftragstyp> Auftragstypen { get; } = Enum.GetValues(typeof(Auftragstyp)).Cast<Auftragstyp>().ToList();
        public bool IsStundenbasiert => this.SelectedAuftragstyp == Auftragstyp.Stundenbasiert;

        [ObservableProperty]
        private string _kundenNamen;



        partial void OnKundenIdChanged(int value)
        {
            _ = LoadKundenDetailsAsync(value);
        }

        private async Task LoadKundenDetailsAsync(int kundenID)
        {
            var kunde = await _databaseService.GetKundeByIdAsync(kundenID);

            if (kunde != null)
            {
                KundenNamen = kunde.KundenName;
            }
        }

        partial void OnStundensatzChanged(decimal value)
        {
            _ = BerechneBetragAsync();
        }

        private async Task BerechneBetragAsync()
        {
            Betrag = Stunden * Stundensatz;
        }

        [RelayCommand]
        private async Task SaveAuftragAsync()
        {
            if (string.IsNullOrWhiteSpace(Auftragsname) || string.IsNullOrWhiteSpace(Auftragsbeschreibung))
            {
                await Shell.Current.DisplayAlert("Fehler", "Bitte geben Sie sowohl den Auftragsnamen als auch die Beschreibung ein.", "OK");
                return;
            }

            var auftrag = new Auftrag
            {
                Auftragsname = this.Auftragsname,
                Beschreibung = this.Auftragsbeschreibung,
                Auftragsdatum = DateTime.Today,
                Stunden = this.Stunden,
                Stundensatz = this.Stundensatz,
                Betrag = this.Betrag,
                Typ = this.SelectedAuftragstyp,
                KundeId = this.KundenId
            };
            int result = await _databaseService.SaveAuftragAsync(auftrag);
            if (result != 0)
            {
                await Shell.Current.DisplayAlert("Erfolg", "Auftrag erfolgreich gespeichert.", "OK");
                var route = $"{nameof(MainPage)}";
                await _navigationService.NavigateToAsync(route);
            }
            else
            {
                await Shell.Current.DisplayAlert("Fehler", "Fehler beim Speichern des Auftrags.", "OK");
            }
        }


    }
}
