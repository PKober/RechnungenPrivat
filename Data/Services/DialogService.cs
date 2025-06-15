using RechnungenPrivat.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechnungenPrivat.Data.Services
{
    class DialogService : IDialogService
    {
        public Task DisplayAlert(string title, string message, string cancel)
        {
            return MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(title, message, cancel));
        }

        public Task<bool> DisplayConfirmation(string title, string message, string accept, string cancel)
        {
            return MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(title, message,accept, cancel));
        }
    }
}
