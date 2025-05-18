using RechnungenPrivat.Views.KundenAnlegen;

namespace RechnungenPrivat
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(KundenAnlegenView), typeof(KundenAnlegenView));
        }
    }
}
