using Microsoft.Extensions.Logging;
using System;
namespace SaintMichel.ViewModel
{
    [QueryProperty(nameof(IDevent), nameof(IDevent))]

    public partial class EventDetailPageViewModel : BaseViewModel
    {
        public int IDevent { get; set; }

        private readonly Event_API _eventApi;


        [ObservableProperty]
        private ObservableCollection<Event> _obsItems;


        public EventDetailPageViewModel(Event_API eventApi)
        {
            _eventApi = eventApi; // Injection de dépendance
            ObsItems = new ObservableCollection<Event>();

        }

      

        private async Task LoadEventDetails(string eventId)
        {
            try
            {
                var items = (await _eventApi.GetEventAsync()).FirstOrDefault(item => item.IDevent == IDevent);
                ObsItems.Add(items);

                //if (eventDetails != null)
                //{


                //    // Convertir la chaîne en DateTime
                //    if (DateTime.TryParse(eventDetails.Date, out var parsedDate))
                //    {
                //        Date = parsedDate;
                //    }
                //    else
                //    {
                //        Debug.WriteLine($"Impossible de convertir la date : {eventDetails.Date}");
                //    }
                //}
                //else
                //{
                //    Debug.WriteLine($"Aucun événement trouvé avec l'ID : {eventId}");
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des détails de l'événement : {ex.Message}");
            }
        }
    }

}
