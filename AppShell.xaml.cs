using RechnungenPrivat.Views.AufträgeFürKundenAnzeigen;
using RechnungenPrivat.Views.AuftragErstellen;
using RechnungenPrivat.Views.AusgabeAnlegen;
using RechnungenPrivat.Views.AusgabenAnzeigen;
using RechnungenPrivat.Views.KundenAnlegen;
using RechnungenPrivat.Views.KundenAnzeigen;
using RechnungenPrivat.Views.KundenLöschen;
using RechnungenPrivat.Views.KundenStatistik;
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
            Routing.RegisterRoute(nameof(AuftragErstellenView), typeof(AuftragErstellenView));
            Routing.RegisterRoute(nameof(AufträgeFürKundenAnzeigenView), typeof(AufträgeFürKundenAnzeigenView));
            Routing.RegisterRoute(nameof(KundenStatistikView), typeof(KundenStatistikView));
            Routing.RegisterRoute(nameof(AusgabenAnzeigenView), typeof(AusgabenAnzeigenView));
            Routing.RegisterRoute(nameof(AusgabeAnlegenView), typeof(AusgabeAnlegenView));
        }


    }
}
