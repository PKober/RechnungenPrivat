using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.AusgabeAnlegen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.ViewModels.AusgabenAnzeigen
{
    public partial class AusgabenAnzeigenViewModel : BaseViewModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<Ausgabe> Ausgaben { get; } = new ObservableCollection<Ausgabe>();


        public AusgabenAnzeigenViewModel(IDatabaseService databaseService, INavigationService navigationService, IDialogService dialogService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        public bool ShowEmptyState => IsNotBusy && Ausgaben.Count == 0;
        public bool ShowAusgabenList => IsNotBusy && Ausgaben.Count > 0; 
        public override async Task InitializeAsync(object? parameter = null)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                OnPropertyChanged(nameof(ShowEmptyState));
                OnPropertyChanged(nameof(ShowAusgabenList));
                var ausgabenListe = await _databaseService.GetAusgabenAsync();

                Ausgaben.Clear();
                foreach (var ausgabe in ausgabenListe.OrderByDescending(a => a.Datum))
                {
                    ausgabenListe.Add(ausgabe);
                }
            }catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Konnte Ausgaben nicht laden: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;

                OnPropertyChanged(nameof(ShowEmptyState));
                OnPropertyChanged(nameof(ShowAusgabenList));
            }
        }


        [RelayCommand]
        private async Task GoToAusgabenAnlegenAsync()
        {
             await _navigationService.NavigateToAsync(nameof(AusgabeAnlegenView));
        }
    }
}
