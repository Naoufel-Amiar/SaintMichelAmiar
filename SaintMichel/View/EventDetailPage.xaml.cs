namespace SaintMichel.View;

public partial class EventDetailPage : ContentPage
{
    public EventDetailPage(EventDetailPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Injecte le ViewModel
    }
}