using RechnungenPrivat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Interfaces
{
    public interface IExcelExportService
    {
        byte[] CreateAuftragsExcelStream(IEnumerable<Auftrag> aufträge, string worksheetTitle);
    }
}
