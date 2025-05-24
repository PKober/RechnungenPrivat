using CommunityToolkit.Mvvm.ComponentModel;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.KundenStatistik
{
    [QueryProperty(nameof(KundenId),"KundenId")]
    public partial class KundenStatistikViewModel : ObservableObject
    {

        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public KundenStatistikViewModel(IDatabaseService databaseService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            Aufträge = new ObservableCollection<Auftrag>();

        }

        [ObservableProperty]
        private int _kundenId;
        [ObservableProperty]
        private string _kundenName;
        [ObservableProperty]
        private int _anzahlAufträge;
        [ObservableProperty]
        private decimal _gesamtBetrag;
        [ObservableProperty]
        private decimal _durchschnittBetrag;
        [ObservableProperty]
        private decimal _durchschnittStunden;
        [ObservableProperty]
        private decimal _durchschnittStundensatz;

        [ObservableProperty]
        private ObservableCollection<Auftrag> _aufträge;

        partial void OnKundenIdChanged(int value)
        {
            _ = LoadAufträgeAsync(value);
        }
        public async Task LoadAufträgeAsync(int kundenId)
        {
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

    }
}
