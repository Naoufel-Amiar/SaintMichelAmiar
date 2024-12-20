namespace SaintMichel.View;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage(ItemDetailPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
