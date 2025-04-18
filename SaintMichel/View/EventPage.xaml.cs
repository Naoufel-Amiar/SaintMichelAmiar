namespace SaintMichel.View;

public partial class EventPage : ContentPage
{
    public EventPage(EventPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Injecte le ViewModel via DI
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as EventPageViewModel)?.LoadItemsEvent();
    }


}