using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System.Collections.ObjectModel;

namespace RechnungenPrivat.ViewModels.AufträgeFürKundenAnzeigenViewModel
{
    [QueryProperty(nameof(KundenId), "KundenId")]
    public partial class AufträgeFürKundenAnzeigenViewModel : BaseViewModel
    {

        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;

        public AufträgeFürKundenAnzeigenViewModel(INavigationService navigationService, IDatabaseService databaseService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
            Aufträge = new ObservableCollection<Auftrag>();
        }
        [ObservableProperty]
        private int _kundenId;

        [ObservableProperty]
        private ObservableCollection<Auftrag> _aufträge;

        [ObservableProperty]
        private string _kundenName;

        [ObservableProperty]
        private Auftrag _selectedAutrag;

        [ObservableProperty]
        private bool _auftragGewählt = false;

        [ObservableProperty]
        private string _auftrasbeschreibung;

        partial void OnKundenIdChanged(int value)
        {
            _ = InitializeAsync();
        }

        public override async Task InitializeAsync(object? parameter = null)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var aufträgeListe = await _databaseService.GetAllAuftraegeByKundeIdAsync(KundenId);
                if (aufträgeListe != null)
                {
                    Aufträge.Clear();
                    foreach (var auftrag in aufträgeListe)
                    {
                        Aufträge.Add(auftrag);
                    }
                }

            }
            catch(Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Fehler beim Laden der Aufträge: {ex.Message}", "OK");
            }finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoToAufträgeDetails(Auftrag auftragParameter)
        {
            Auftrag auftragFürAktion = null;

            if (auftragParameter != null)
            {
                auftragFürAktion = auftragParameter;
            }
            else if (SelectedAutrag != null)
            {
                auftragFürAktion = SelectedAutrag;
            }

            if (auftragFürAktion == null)
                return;
            AuftragGewählt = true;

            // await Shell.Current.DisplayAlert("Ausgewählter Auftrag",
            //     $"Name: {auftragFürAktion.Auftragsname}\nBeschreibung: {auftragFürAktion.Beschreibung}", "OK");

        }

        [RelayCommand]
        private async Task DeleteAuftragAsync()
        {
            if (SelectedAutrag == null)
            {
                await _dialogService.DisplayAlert("Fehler", "Bitte wählen Sie einen Auftrag zum Löschen aus", "OK");
                return;
            }
            var result = await _dialogService.DisplayConfirmation("Bestätigung",
                $"Möchten Sie den Auftrag '{SelectedAutrag.Auftragsname}' wirklich löschen?", "Ja", "Nein");
            if (result)
            {
                await _databaseService.DeleteAuftragAsync(SelectedAutrag);
                Aufträge.Remove(SelectedAutrag);
                SelectedAutrag = null;
                AuftragGewählt = false;
            }

        }
    }
}
