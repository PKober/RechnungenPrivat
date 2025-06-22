using CommunityToolkit.Mvvm.ComponentModel;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.AusgabeDetail
{
    [QueryProperty(nameof(AusgabeId),"AusgabeId")]
    public partial class AusgabeDetailViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        //private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private int _selectedAusgabeId;
        public AusgabeDetailViewModel(IDatabaseService databaseService, /*INavigationService navigationService ,*/IDialogService dialogService)
        {
            _databaseService = databaseService;
        //    _navigationService = navigationService;
            _dialogService = dialogService;
        }
        

        [ObservableProperty]
        private int _ausgabeId ;

        private int _uebergebeneId;

        [ObservableProperty]
        private Ausgabe? _ausgabe;

        partial void OnAusgabeIdChanged(int value)
        {
            if(value != null)
            {
                _selectedAusgabeId = value;
                _ = InitializeAsync();
            }else
            {
                _dialogService.DisplayAlert("Fehler", "Eine ungültige ID wurde übergeben.", "OK");
            }
        }

        public override async Task InitializeAsync(object? parameter = null)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                

              Ausgabe =  await _databaseService.GetAusgabeByIdAsync(_selectedAusgabeId);
                
            }
            catch(System.Exception ec)
            {
                await _dialogService.DisplayAlert("Fehler", $"Ausgabe konnte nicht geladen werden:{ec.Message} ", "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

    }
}
