using RechnungenPrivat.ViewModels.KundenAnlegen;
using RechnungenPrivat.ViewModels.KundenLöschen;

namespace RechnungenPrivat.Views.KundenLöschen;

public partial class KundenLöschenView : ContentPage
{
	public KundenLöschenView(KundenLöschenViewModel kundenLöschenViewModel)
	{
		InitializeComponent();
        BindingContext = kundenLöschenViewModel;

    }
}