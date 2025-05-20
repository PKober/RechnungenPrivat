using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.KundenAnzeigen;
using RechnungenPrivat.Views.KundenLöschen;
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
            Routing.RegisterRoute(nameof(KundenLöschenView), typeof(KundenLöschenView));
            Routing.RegisterRoute(nameof(KundenAnzeigenView), typeof(KundenAnzeigenView));
        }


    }
}
