using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using RechnungenPrivat.Views.AusgabeAnlegen;
using RechnungenPrivat.Views.AusgabeDetail;
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
        [ObservableProperty]
        private Ausgabe? _selectedAusgabe;

        public AusgabenAnzeigenViewModel(IDatabaseService databaseService, INavigationService navigationService, IDialogService dialogService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _dialogService = dialogService;
           _ =  InitializeAsync();
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
                MainThread.BeginInvokeOnMainThread(() =>
                {

                    Ausgaben.Clear();
                    foreach (var ausgabe in ausgabenListe.OrderByDescending(a => a.Datum))
                    {
                        Ausgaben.Add(ausgabe);
                    }
                });
            }catch (System.Exception ex)
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

        [RelayCommand]
        private async Task DeleteAusgabeAsync(Ausgabe? ausgabeToDelete)
        {
            if(ausgabeToDelete == null)
            {
                return;
            }

            bool confirmed = await _dialogService.DisplayConfirmation("Ausgabe Löschen ?",$"Möchten Sie {ausgabeToDelete.Bezeichnung} Löschen?","Ja","Nein");
            try { 
            if(confirmed == true)
            {
                await _databaseService.DeleteAusgabeAsync(ausgabeToDelete);
                Ausgaben.Remove(ausgabeToDelete);
                OnPropertyChanged(nameof(ShowEmptyState));
                OnPropertyChanged(nameof(ShowAusgabenList));
            }else
            {
                return;
            }
            }
            catch(System.Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Die Ausgabe konnte nicht gelöscht werden: {ex.Message}", "Ok");
            }
        }

        [RelayCommand]
        private async void SelectAusgabeAsync(Ausgabe? value)
        {
            if (value == null) return;

            var keyValues = new Dictionary<string, object> {{"AusgabeId",value.ID}};
            _navigationService.NavigateToAsync($"{nameof(AusgabeDetailView)}",keyValues);

            
        }
    }
}
