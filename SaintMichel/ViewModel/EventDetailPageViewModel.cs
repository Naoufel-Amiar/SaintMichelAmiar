using Microsoft.Extensions.Logging;
using System;
namespace SaintMichel.ViewModel
{
    [QueryProperty(nameof(IDEvent), nameof(IDEvent))]
    public partial class EventDetailPageViewModel : BaseViewModel
    {
        private int _idEvent;

        public int IDEvent
        {
            get => _idEvent;
            set
            {
                if (_idEvent != value)
                {
                    _idEvent = value;
                    OnPropertyChanged();
                    LoadEvent(); // ← appel automatique
                }
            }
        }

        [ObservableProperty]
        private ObservableCollection<Event> obsItems = new();

        private readonly Event_API _api;

        public EventDetailPageViewModel(Event_API api)
        {
            _api = api;
        }

        private async void LoadEvent()
        {
            IsBusy = true;
            try
            {
                ObsItems.Clear();
                var item = await _api.GetEventByIdAsync(IDEvent.ToString());
                if (item != null)
                    ObsItems.Add(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erreur", "Impossible de charger l'événement", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
