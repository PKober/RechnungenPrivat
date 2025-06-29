using RechnungenPrivat.Data.Interfaces;
using RechnungenPrivat.Models;
using System.Globalization;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace RechnungenPrivat.Data.Services
{
    internal class RechnungsService : IRechnungsService
    {
        public async Task<byte[]> ErstelleRechnungWordAsync(Kunde kunde, IEnumerable<Auftrag> aufträge)
        {
            using var wordVorlage = await FileSystem.OpenAppPackageFileAsync("Resources/Templates/VorlageRathelbeckerweg42.docx");
            using var document = DocX.Load(wordVorlage);

            ErsetzeStammdaten(document, kunde);
            ErstellePositionsTabelle(document, aufträge);
            BerechneBetrag(document,aufträge);
            decimal gesamtbetrag = 0;


            using var stream = new MemoryStream();

            document.SaveAs(stream);

            stream.Position = 0;

            return stream.ToArray();
        }

        private void ErsetzeStammdaten(Document doc, Kunde kunde)
        {
            if (doc != null)
            {
                var replaceOptions = new StringReplaceTextOptions
                {
                    SearchValue = "{{KUNDENNAME}}",
                    NewValue = kunde.KundenName
                };
                doc.ReplaceText(replaceOptions);

                replaceOptions = new StringReplaceTextOptions
                {
                    SearchValue = "{{KUNDENADRESSE}}",
                    NewValue = kunde.KundenAdresse
                };

                doc.ReplaceText(replaceOptions);

                replaceOptions = new StringReplaceTextOptions
                {
                    SearchValue = "{{AUFTRAGSMONAT}}",
                    NewValue = DateTime.UtcNow.ToString("MMMM",CultureInfo.CurrentCulture)
                };

                doc.ReplaceText(replaceOptions);

                replaceOptions = new StringReplaceTextOptions
                {
                    SearchValue = "{{DATUM}}",
                    NewValue = DateTime.UtcNow.ToShortDateString()
                };

                doc.ReplaceText(replaceOptions);
            }
        }

        private void ErstellePositionsTabelle(Document doc, IEnumerable<Auftrag> aufträge)
        {
            Paragraph platzhalterAbsatz = null ;
            if(doc != null)
            {
                foreach (var item in doc.Paragraphs)
                {
                    if(item.Text == "{{PositionTabelle}}")
                    {
                        platzhalterAbsatz = item;
                        break;
                    }
                }
                
                int spaltenAnzahl = 4;
                int zeilenAnzahl = aufträge.Count() + 1;

                var positionsTabelle = doc.AddTable(zeilenAnzahl,spaltenAnzahl);

                positionsTabelle.Rows[0].Cells[0].Paragraphs.First().Append("Menge").Bold().Alignment = Alignment.center;
                positionsTabelle.Rows[0].Cells[1].Paragraphs.First().Append("Beschreibung").Bold().Alignment = Alignment.center;
                positionsTabelle.Rows[0].Cells[2].Paragraphs.First().Append("Einzelpreis").Bold().Alignment = Alignment.center;
                positionsTabelle.Rows[0].Cells[3].Paragraphs.First().Append("Zeilensumme").Bold().Alignment = Alignment.center;
                int zeilenIndex = 1;

                foreach (var auftrag in aufträge)
                {
                    var aktuelleZeile = positionsTabelle.Rows[zeilenIndex];
                    decimal? menge;
                    decimal? einzelpreis;
                    decimal? zeilenSumme;
                    if(auftrag.Typ == Auftragstyp.Stundenbasiert)
                    {
                        menge = auftrag.Stunden;
                        einzelpreis = auftrag.Stundensatz;
                        zeilenSumme = auftrag.Stunden * auftrag.Stundensatz;
                    }
                    else
                    {
                        menge = 1;
                        einzelpreis = auftrag.Betrag;
                        zeilenSumme = auftrag.Betrag;
                    }

                    aktuelleZeile.Cells[0].Paragraphs.First().Append(menge.ToString());
                    aktuelleZeile.Cells[1].Paragraphs.First().Append(auftrag.Auftragsname);
                    aktuelleZeile.Cells[2].Paragraphs.First().Append(einzelpreis.ToString() + " €");
                    aktuelleZeile.Cells[3].Paragraphs.First().Append(zeilenSumme.ToString() + " €");
                    zeilenIndex++;
                
                }

                platzhalterAbsatz.InsertTableAfterSelf(positionsTabelle);
                platzhalterAbsatz.Remove(false);

            }


        }

        private void BerechneBetrag(Document doc, IEnumerable<Auftrag> aufträge)
        {
            decimal? betrag = 0 ;
            foreach (var auftrag in aufträge)
            {
                betrag += auftrag.Betrag;
            }

            var replaceOptions = new StringReplaceTextOptions()
            {
                SearchValue = "{{Betrag}}",
                NewValue = betrag.ToString()
            };

            doc.ReplaceText(replaceOptions);
        }
    }
}
