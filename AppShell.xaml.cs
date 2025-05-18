using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.Startseite;

namespace RechnungenPrivat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(KundenAnlegenView), typeof(KundenAnlegenView));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
