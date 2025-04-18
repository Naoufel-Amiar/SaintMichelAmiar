

namespace SaintMichel.ViewModel
{
    public partial class EventPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Event> _obsItemsEvents; //Items a afficher dans le list view

        [ObservableProperty]
        private string userInput; // Propriété pour capturer le texte de l'Entry

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Now; // Initialisation à la date actuelle


        //[ObservableProperty]
        //private Event? selectedEvent;


        [ObservableProperty]
        private Event _selectedItem;


        Event_API _EventApi;

        public EventPageViewModel(Event_API apievent)
        {

            _EventApi = apievent;
            ObsItemsEvents = new ObservableCollection<Event>(); // Initialisation de la collection Observable

        }

        // Méthode pour charger les éléments depuis l'API
        async void OnAppearing()
        {

            await LoadItemsEvent();
        }

        [RelayCommand]
        public async Task LoadItemsEvent()
        {
            IsBusy = true;
            try
            {
                ObsItemsEvents.Clear();

                var items = await _EventApi.GetEventAsync();

                if (items != null)
                {
                    // Filtrer les événements à venir (Date >= aujourd’hui)
                    var upcomingEvents = items
                        .Where(item =>
                        {
                            if (DateTime.TryParse(item.Date, out DateTime eventDate))
                                return eventDate >= DateTime.Now;
                            return false;
                        })
                        .OrderBy(item => DateTime.Parse(item.Date)) // Optionnel : tri par date croissante
                        .ToList();

                    foreach (var item in upcomingEvents)
                    {
                        ObsItemsEvents.Add(item);
                    }

                    if (upcomingEvents.Count == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Information", "Aucun événement à venir trouvé.", "OK");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Information", "Aucun événement trouvé.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erreur", "Données non récupérées.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task LoadSearchByCityBtn()
        {
            IsBusy = true;
            try
            {
                ObsItemsEvents.Clear();

                // Utilisez la propriété City pour récupérer la valeur saisie
                string ville = userInput;

                // Vérifiez si la ville est saisie
                if (string.IsNullOrWhiteSpace(ville))
                {
                    await Application.Current.MainPage.DisplayAlert("Erreur", "Veuillez entrer une ville.", "OK");
                    return;
                }

                var items = await _EventApi.GetEventByAreaAsync(ville);

                if (items != null && items.Any()) // Vérification si des données ont été récupérées
                {
                    foreach (var item in items)
                    {
                        ObsItemsEvents.Add(item);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Information", $"Aucun événement trouvé pour {ville}.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}"); // Afficher les erreurs dans la console
                await Application.Current.MainPage.DisplayAlert("Erreur", "Donnée Non récupérée", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }



        [RelayCommand]
        async Task ResetBtn()
        {
            IsBusy = true;
            try
            {

                UserInput = string.Empty;

                ObsItemsEvents.Clear();
                //DateTime selectedDate = new DateTime(2024, 10, 01);
                //var items = await _EventApi.GetEventByDateAsync(selectedDate);                 

                var items = await _EventApi.GetEventAsync();

                if (items != null) // Vérification si des données ont été récupérées
                {
                    foreach (var item in items)
                    {
                        ObsItemsEvents.Add(item);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Information", "Aucun événement trouvé", "OK");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}"); // Afficher les erreurs dans la console
                await Application.Current.MainPage.DisplayAlert("Erreur", "Donnée Non recuperer", "OK");
            }

            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task LoadSearchByDateBtn()
        {
            IsBusy = true;
            try
            {
                ObsItemsEvents.Clear();

                // Convertir la date sélectionnée au format "YYYY-MM-DD"
                string formattedDate = selectedDate.ToString("yyyy-MM-dd");

                // Appeler l'API avec la date formatée
                var items = await _EventApi.GetEventByDateAsync(formattedDate);

                if (items != null && items.Any()) // Vérification si des données ont été récupérées
                {
                    foreach (var item in items)
                    {
                        ObsItemsEvents.Add(item);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Information", $"Aucun événement trouvé pour la date : {formattedDate}.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur : {ex.Message}"); // Afficher les erreurs dans la console
                await Application.Current.MainPage.DisplayAlert("Erreur", "Impossible de récupérer les données.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }



        [RelayCommand]
        async Task EventSelected()
        {
            if (SelectedItem == null)
            {
                return;
            }
            await Shell.Current.GoToAsync($"{nameof(EventDetailPage)}?{nameof(EventDetailPageViewModel.IDEvent)}={SelectedItem.IDevent}");
        }

    }
}
