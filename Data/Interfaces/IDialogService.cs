using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Interfaces
{
    public interface IDialogService
    {
        /// <summary>
        /// Zeigt einfache Benachrichtigung an 
        /// </summary>
        /// <param name="title">Der titel des Dialogs</param>
        /// <param name="message">Die Nachricht</param>
        /// <param name="cancel">Der Text des Abbrechen Buttons</param>
        /// <returns></returns>
        Task DisplayAlert(string title, string message, string cancel);

        /// <summary>
        /// Zeigt eine Bestätigungsabfrage mit "Ja" und "Nein" an
        /// </summary>
        /// <param name="title">Der Titel</param>
        /// <param name="message">Die Nachricht</param>
        /// <param name="accept">Der Text des Akzeptieren Buttons</param>
        /// <param name="cancel">Der Text des Abbrechen Buttons</param>
        /// <returns>Gibt True zurück, wenn der Benutzer auf "accept" klickt, andernfalls false</returns>
        Task<bool> DisplayConfirmation(string title,string message, string accept,string cancel);
    }
}
