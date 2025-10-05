using RechnungenPrivat.ViewModels.PrivateSeiten.PrivateSeiteStarteSeite;

namespace RechnungenPrivat.Views.PrivateSeiten.PrivateSeitenStartSeite;

public partial class PrivateSeiteStarteSeiteView : ContentPage
{
	public PrivateSeiteStarteSeiteView(PrivateSeiteStarteSeiteViewModel privateSeiteStarteSeiteViewModel)
	{
		InitializeComponent();
		BindingContext = privateSeiteStarteSeiteViewModel;
	}


}