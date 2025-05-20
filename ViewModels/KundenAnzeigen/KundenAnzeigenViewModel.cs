using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;

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


    }
}
