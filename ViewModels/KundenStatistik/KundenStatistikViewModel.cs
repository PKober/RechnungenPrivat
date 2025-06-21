using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Maui
using ClosedXML.Excel;
using System.IO;


namespace RechnungenPrivat.ViewModels.KundenStatistik
{
    [QueryProperty(nameof(KundenId), "KundenId")]
    public partial class KundenStatistikViewModel : BaseViewModel
    {

        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;
        private readonly IExcelExportService _excelExportService;
        private readonly IDialogService _dialogService;

        public KundenStatistikViewModel(IDatabaseService databaseService, INavigationService navigationService,IExcelExportService excelExportService,IDialogService dialogService)
        {
            _databaseService = databaseService;
            _navigationService = navigationService;
            _excelExportService = excelExportService;
            _dialogService = dialogService;


            Aufträge = new ObservableCollection<Auftrag>();
            GefilterteAufträge = new ObservableCollection<Auftrag>();
            Jahre = new ObservableCollection<int?>();
            Monate = new ObservableCollection<MonatItem>();

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
        private ObservableCollection<Auftrag> _aufträge;

        [ObservableProperty]
        private ObservableCollection<Auftrag> _gefilterteAufträge;

        private List<Auftrag> _alleKundenAufträge = new();
        private Kunde _kunde;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ApplyFilterCommand))]
        private int? _selectedJahr;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExportToExcelCommand))]
        private MonatItem _selectedMonatItem;
        

        public ObservableCollection<int?> Jahre { get; }
        
        public ObservableCollection<MonatItem> Monate { get; }

        partial void OnKundenIdChanged(int value)
        {
            _ = InitializeAsync();

        }
        partial void OnSelectedJahrChanged(int? value)
        {
            ApplyFilter();
        }

        partial void OnSelectedMonatItemChanged(MonatItem value)
        {
            ApplyFilter();
        }

        public override async Task InitializeAsync(object? parameter = null)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                // Kundendetails laden
                var kunde = await _databaseService.GetKundeByIdAsync(KundenId);
                if (kunde != null)
                {
                    KundenName = kunde.KundenName;
                }

                // Aufträge laden und berechnen
                var aufträgeListe = await _databaseService.GetAllAuftraegeByKundeIdAsync(KundenId);
                if (aufträgeListe != null)
                {
                    Aufträge.Clear();
                    GesamtBetrag = 0;
                    DurchschnittStunden = 0;

                    foreach (var auftrag in aufträgeListe)
                    {
                        Aufträge.Add(auftrag);
                        GesamtBetrag += auftrag.Betrag;
                        DurchschnittStunden += (decimal)auftrag.Stunden;
                    }

                    AnzahlAufträge = Aufträge.Count;
                    if (AnzahlAufträge > 0)
                    {
                        DurchschnittBetrag = GesamtBetrag / AnzahlAufträge;
                    }
                }

                // Filter initialisieren (nur wenn noch nicht geschehen)
                if (Monate.Count == 0)
                {
                    InitializeFilterData();
                }

                // Initialfilter anwenden
                ApplyFilter();
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Die Statistik-Daten konnten nicht geladen werden: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
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
                    GesamtBetrag += auftrag.Betrag;
                    DurchschnittStunden += (decimal)auftrag.Stunden;
                    
                }
                DurchschnittBetrag = GesamtBetrag / Aufträge.Count - 1;
                
                AnzahlAufträge = Aufträge.Count;
                ApplyFilter();
            }
        }

        public class MonatItem
        {
            public string Name { get; set; }
            public int? Wert { get; set; } // Nullable für "Alle" Option
        }

        private void InitializeFilterData()
        {
            Monate.Add(new MonatItem
            {
                Name = "Alle Monate",
                Wert = null
            });
            for (int i = 1; i <= 12; i++)
            {
                Monate.Add(new MonatItem
                {
                    Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                    Wert = i
                });
            }

            Jahre.Add(null);
            int aktuellesJahr = DateTime.Now.Year;
            for (int i = aktuellesJahr; i >= aktuellesJahr - 10  ; i--)
            {
                Jahre.Add(i);
            }

            SelectedMonatItem = Monate.FirstOrDefault();
            SelectedJahr = Jahre.FirstOrDefault(1);
        }

        [RelayCommand]
        private void ApplyFilter()
        {
            if (Aufträge == null)
            {
                return;
            }

            IEnumerable<Auftrag> tempGefilterteAufträge = Aufträge;

            if (SelectedJahr.HasValue )
            {
                tempGefilterteAufträge = tempGefilterteAufträge.Where(a => a.Auftragsdatum.HasValue && a.Auftragsdatum.Value.Year == SelectedJahr.Value);
            }

            if (SelectedMonatItem?.Wert.HasValue ?? false)
            {
                tempGefilterteAufträge = tempGefilterteAufträge.Where(a => a.Auftragsdatum.HasValue && a.Auftragsdatum.Value.Month == SelectedMonatItem.Wert.Value);
            }

            GefilterteAufträge.Clear();
            foreach (var auftrag in tempGefilterteAufträge.OrderByDescending(a => a.Auftragsdatum))
            {
                GefilterteAufträge.Add(auftrag);
            }
            ExportToExcelCommand.NotifyCanExecuteChanged();
        }

        private bool CanExportToExcel()
        {
            return GefilterteAufträge.Any();
        }

        [RelayCommand(CanExecute = nameof(CanExportToExcel))]
        private async Task ExportToExcelAsync(CancellationToken cancellationToken)
        {
            if (!CanExportToExcel()) return;

            try
            {
                var worksheetTitle = $"Aufträge für {KundenName}";
                byte[] exceldata = _excelExportService.CreateAuftragsExcelStream(GefilterteAufträge, worksheetTitle);

                using var stream = new MemoryStream(exceldata);

                var fileName = $"Statistik_{KundenName}_{KundenId}.{DateTime.Today.Month.ToString()}.xlsx";
                var fileSaverResult = await FileSaver.Default.SaveAsync(fileName, stream, cancellationToken);
                if (!fileSaverResult.IsSuccessful)
                {
                    await _dialogService.DisplayAlert("Fehler", $"Die Datei konnte nicht gespeichert werden: {fileSaverResult.Exception.Message}", "OK");
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlert("Fehler", $"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "OK");
            }
        }
    }
}


