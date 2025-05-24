using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.AufträgeFürKundenAnzeigen;
using RechnungenPrivat.Views.AuftragErstellen;
using System.Collections.ObjectModel;


namespace RechnungenPrivat.ViewModels.KundenAnzeigen
{
    public partial class KundenAnzeigenViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;

        public KundenAnzeigenViewModel(INavigationService navigationService, IDatabaseService databaseService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            Kunden = new ObservableCollection<Kunde>();
        }

        [ObservableProperty]
        private ObservableCollection<Kunde> _kunden;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private Kunde _selectedKunde;

        [ObservableProperty]
        private bool _kundeGewählt = false;

        public async Task LoadKundenAsync()
        {
            IsRefreshing = true;
            var kundenListe = await _databaseService.GetAllKundenAsync();
            if (kundenListe != null)
            {
                Kunden.Clear();
                foreach (var kunde in kundenListe)
                {
                    Kunden.Add(kunde);
                }
            }
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task GoToKundenDetails(Kunde kundeParameter)
        {

            Kunde kundeFürAktion = null;

            if (kundeParameter != null)
            {
                
                kundeFürAktion = kundeParameter;
            }
            else if (SelectedKunde != null)
            {
                kundeFürAktion = SelectedKunde;
            }

            if (kundeFürAktion == null)
                return;
            KundeGewählt = true;
            
            //await Shell.Current.DisplayAlert("Ausgewählter Kunde", $"Name: {kundeFürAktion.KundenName}\nAdresse: {kundeFürAktion.KundenAdresse}", "OK");


        }

        [RelayCommand]
        private async Task AuftragErstellenFuerKundenAsync()
        {
            if (SelectedKunde == null)
            {
                await Shell.Current.DisplayAlert("Fehler", "Bitte wählen Sie zuerst einen Kunden aus", "Ok");
                return; 
            }

            await _navigationService.NavigateToAsync($"{nameof(AuftragErstellenView)}?KundenId={SelectedKunde.Id}");
        }

        [RelayCommand]
        private async Task AufträgeAnzeigenFuerKundenAsync()
        {
            if (SelectedKunde == null)
            {
                await Shell.Current.DisplayAlert("Fehler", "Bitte wählen Sie zuerst einen Kunden aus", "Ok");
                return;
            }

            await _navigationService.NavigateToAsync($"{nameof(AufträgeFürKundenAnzeigenView)}?KundenId={SelectedKunde.Id}");
        }

    }
}
