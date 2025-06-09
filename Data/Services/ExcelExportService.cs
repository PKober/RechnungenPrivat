using ClosedXML.Excel;
using CommunityToolkit.Maui.Storage;
using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Services
{
    class ExcelExportService : IExcelExportService
    {
        public byte[] CreateAuftragsExcelStream(IEnumerable<Auftrag> aufträge, string worksheetTitle)
        {

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(worksheetTitle);

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
            decimal? gesamtStunden = 0;

            foreach (Auftrag auftrag in aufträge)
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

            worksheet.Row(currentRow + 10).Style.Font.Bold = true;

            worksheet.Column("B").Style.DateFormat.Format = "dd.MM.yyyy";
            worksheet.Column("D").Style.NumberFormat.Format = "#,##0.00 €";
            worksheet.Column("F").Style.NumberFormat.Format = "#,##0.00";
            worksheet.Column("G").Style.NumberFormat.Format = "#,##0.00 €";
            worksheet.Cell(currentRow + 11, 1).Style.NumberFormat.Format = "#,##0.00 €";
            worksheet.Cell(currentRow + 11, 3).Style.NumberFormat.Format = "#,##0.00";
            worksheet.Columns().AdjustToContents();
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            stream.Position = 0;
            return stream.ToArray();
            
        }
    }
}
