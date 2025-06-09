using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Maui.Storage;
using ClosedXML.Excel;
using System.IO;


namespace RechnungenPrivat.ViewModels.KundenStatistik
{
    [QueryProperty(nameof(KundenId), "KundenId")]
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
        private ObservableCollection<Auftrag> _aufträge;

        [ObservableProperty]
        private ObservableCollection<Auftrag> _gefilterteAufträge = new();

        private List<Auftrag> _alleKundenAufträge = new();
        private Kunde _kunde;
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ApplyFilterCommand))]
        private int? _selectedJahr;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ExportToExcelCommand))]
        private MonatItem _selectedMonatItem;
        

        public ObservableCollection<int?> Jahre { get; } = new();
        
        public ObservableCollection<MonatItem> Monate { get; } = new();

        partial void OnKundenIdChanged(int value)
        {
            _ = LoadAufträgeAsync(value);
            _ = LoadKundenDetailsAsync(value);
            InitializeFilterData();

            this.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(SelectedJahr) || e.PropertyName == nameof(SelectedMonatItem))
                {
                    ApplyFilter();
                }
            };

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
        public async Task LoadKundenDetailsAsync(int kundenId)
        {
            var kunde = await _databaseService.GetKundeByIdAsync(kundenId);
            if (kunde != null)
            {
                _kunde = kunde;
                KundenName = kunde.KundenName;
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
        }

        private bool CanExportToExcel()
        {
            return GefilterteAufträge.Any();
        }

        [RelayCommand(CanExecute = nameof(CanExportToExcel))]
        private async Task ExportToExcelAsync(CancellationToken cancellationToken)
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Auftragsstatistik");

                worksheet.Cell("A1").Value = "Auftragsname";
                worksheet.Cell("B1").Value = "Datum";
                worksheet.Cell("C1").Value = "Beschreibung";
                worksheet.Cell("D1").Value = "Betrag";
                worksheet.Cell("E1").Value = "Typ";
                worksheet.Cell("F1").Value = "Stunden";
                worksheet.Cell("G1").Value = "Stundensatz";

                worksheet.Row(1).Style.Font.Bold = true;
                int currentRow = 2;
                decimal gesamtBetrag = 0;
                decimal? gesamtStunden= 0;

                foreach(Auftrag auftrag in GefilterteAufträge)
                {
                    worksheet.Cell(currentRow, 1).Value = auftrag.Auftragsname;
                    worksheet.Cell(currentRow, 2).Value = auftrag.Auftragsdatum;
                    worksheet.Cell(currentRow, 3).Value = auftrag.Beschreibung;
                    worksheet.Cell(currentRow, 4).Value = auftrag.Betrag;
                    worksheet.Cell(currentRow, 5).Value = auftrag.Typ.ToString();
                    worksheet.Cell(currentRow, 6).Value = auftrag.Stunden;
                    worksheet.Cell(currentRow, 7).Value = auftrag.Stundensatz;
                    currentRow++;
                    gesamtBetrag += auftrag.Betrag;
                    gesamtStunden += auftrag.Stunden;
                }

                
                worksheet.Cell(currentRow + 10, 1).Value = "Gesamtbetrag";
                worksheet.Cell(currentRow + 11, 1).Value = gesamtBetrag;

                worksheet.Cell(currentRow + 10, 3).Value = "Gesamtstunden";
                worksheet.Cell(currentRow + 11, 3).Value = gesamtStunden;

                worksheet.Row(currentRow+10).Style.Font.Bold = true;

                worksheet.Column("B").Style.DateFormat.Format = "dd.MM.yyyy";
                worksheet.Column("D").Style.NumberFormat.Format = "#,##0.00 €";
                worksheet.Column("F").Style.NumberFormat.Format = "#,##0.00";
                worksheet.Column("G").Style.NumberFormat.Format = "#,##0.00 €";
                worksheet.Cell(currentRow+11,1).Style.NumberFormat.Format = "#,##0.00 €";
                worksheet.Cell(currentRow+11,3).Style.NumberFormat.Format = "#,##0.00";
                worksheet.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);

                stream.Position = 0;

                var fileName = $"Statistik_{KundenName}_{KundenId}.{DateTime.Today.Month.ToString()}.xlsx";
                var fileSaverResult = await FileSaver.Default.SaveAsync(fileName, stream, cancellationToken);
                if (!fileSaverResult.IsSuccessful)
                {
                    await Shell.Current.DisplayAlert("Fehler", $"Die Datei konnte nicht gespeichert werden: {fileSaverResult.Exception.Message}", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fehler", $"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}", "OK");
            }
        }
    }
}

