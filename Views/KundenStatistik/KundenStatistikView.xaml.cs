using RechnungenPrivat.ViewModels.KundenStatistik;

namespace RechnungenPrivat.Views.KundenStatistik;

public partial class KundenStatistikView : ContentPage
{
    public KundenStatistikView(KundenStatistikViewModel kundenStatistikViewModel)
    {
        InitializeComponent();
        BindingContext = kundenStatistikViewModel;
    }
}