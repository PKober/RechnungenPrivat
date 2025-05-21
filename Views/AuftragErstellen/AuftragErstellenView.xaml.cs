using RechnungenPrivat.ViewModels.AuftragErstellen;
using RechnungenPrivat.ViewModels.KundenAnlegen;

namespace RechnungenPrivat.Views.AuftragErstellen;

public partial class AuftragErstellenView : ContentPage
{
	public AuftragErstellenView(AuftragErstellenViewModel auftragErstellenViewModel)
	{
		InitializeComponent();
		BindingContext = auftragErstellenViewModel;
	}
}