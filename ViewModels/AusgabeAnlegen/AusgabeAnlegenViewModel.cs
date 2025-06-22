using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.AusgabeAnlegen
{
    public partial class AusgabeAnlegenViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly IMediaService _mediaService;

        public AusgabeAnlegenViewModel(
            IDatabaseService databaseService,
            INavigationService navigationService,
            IDialogService dialogService,
            IMediaService mediaService
            )
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _dialogService = dialogService;
            _mediaService = mediaService;

        }
        [ObservableProperty]
        private string bezeichnung;

        [ObservableProperty]
        private decimal betrag;

        [ObservableProperty]
        private DateTime datum;

        [ObservableProperty]
        private string notizen;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(BelegFotoSource))] 
        private byte[]? belegFoto;

        public ImageSource? BelegFotoSource => belegFoto == null ? null : ImageSource.FromStream(() => new MemoryStream(belegFoto));

        [RelayCommand]
        private async Task CapturePhotoAsync()
        {
            var fotoData = await _mediaService.CapturePhotoAsync();
            if (fotoData != null)
            {
                BelegFoto = fotoData;
            }
        }

        [RelayCommand]
        private async Task PickPhotoAsync()
        {
            var fotoData = await _mediaService.PickPhotoAsync();
            if (fotoData != null)
            {
                BelegFoto = fotoData;
            }
        }

        [RelayCommand]
        private async Task SaveAusgabeAsync()
        {
            if (string.IsNullOrWhiteSpace(Bezeichnung) || Betrag <= 0 || BelegFoto == null)
            {
                await _dialogService.DisplayAlert("Fehler", "Bitte füllen Sie Bezeichnung und Betrag aus und fügen Sie einen Beleg hinzu.", "OK");
                return;
            }

            var neueAusgabe = new Ausgabe
            {
                Bezeichnung = this.Bezeichnung,
                Betrag = this.Betrag,
                Datum = this.Datum,
                Notizen = this.Notizen,
                BelegFoto = this.BelegFoto
            };

            var result = await _databaseService.SaveAusgabeAsync(neueAusgabe);

            if (result != 0)
            {
                await _dialogService.DisplayAlert("Erfolg", "Die Ausgabe wurde erfolgreich gespeichert.", "OK");
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _dialogService.DisplayAlert("Fehler", "Die Ausgabe konnte nicht gespeichert werden.", "OK");
            }
        }

    }
}
