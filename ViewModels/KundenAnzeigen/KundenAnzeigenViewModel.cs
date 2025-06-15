using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Data.Services;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.AufträgeFürKundenAnzeigen;
using RechnungenPrivat.Views.AuftragErstellen;
using RechnungenPrivat.Views.KundenStatistik;
using System.Collections.ObjectModel;


namespace RechnungenPrivat.ViewModels.KundenAnzeigen
{
    public partial class KundenAnzeigenViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;

        public KundenAnzeigenViewModel(INavigationService navigationService, IDatabaseService databaseService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _databaseService = databaseService;
            _dialogService = dialogService;
        
        Kunden = new ObservableCollection<Kunde>();
        }

        [ObservableProperty]
        private ObservableCollection<Kunde> _kunden;

        //[ObservableProperty]
        //private bool _isRefreshing;

        [ObservableProperty]
        private Kunde _selectedKunde;

        [ObservableProperty]
        private bool _kundeGewählt = false;

        public override async Task InitializeAsync(object? parameter = null)
        {
            await LoadKundenAsync();
        }

        public async Task LoadKundenAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
            var kundenListe = await _databaseService.GetAllKundenAsync();
            if (kundenListe != null)
            {
                Kunden.Clear();
                foreach (var kunde in kundenListe)
                {
                    Kunden.Add(kunde);
                }
            }
            }catch(Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Ein Fehler ist beim Laden der Kunden aufgetreten: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }

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
                await _dialogService.DisplayAlert("Fehler", "Bitte wählen Sie zuerst einen Kunden aus", "Ok");
                return;
            }

            await _navigationService.NavigateToAsync($"{nameof(AuftragErstellenView)}?KundenId={SelectedKunde.Id}");
        }

        [RelayCommand]
        private async Task AufträgeAnzeigenFuerKundenAsync()
        {
            if (SelectedKunde == null)
            {
                await _dialogService.DisplayAlert("Fehler", "Bitte wählen Sie zuerst einen Kunden aus", "Ok");
                return;
            }

            await _navigationService.NavigateToAsync($"{nameof(AufträgeFürKundenAnzeigenView)}?KundenId={SelectedKunde.Id}");
        }


        [RelayCommand]
        private async Task GotoStatistikAsync()
        {
            if (SelectedKunde == null)
            {
                await _dialogService.DisplayAlert("Fehler", "Bitte wählen Sie zuerst einen Kunden aus", "Ok");
                return;
            }
            else if (SelectedKunde != null)
            {
                await _navigationService.NavigateToAsync($"{nameof(KundenStatistikView)}?KundenId={SelectedKunde.Id}");
            }

        }
    }
}
