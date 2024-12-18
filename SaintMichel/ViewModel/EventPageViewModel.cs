

namespace SaintMichel.ViewModel
{
    public partial class EventPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<Event> _obsItemsEvents; //Items a afficher dans le list view

        [ObservableProperty]
        private string userInput; // Propriété pour capturer le texte de l'Entry

        Event_API _EventApi;

        public EventPageViewModel(Event_API apievent)
        {

            _EventApi = apievent;
            ObsItemsEvents = new ObservableCollection<Event>(); // Initialisation de la collection Observable
            OnAppearing();
        }

        // Méthode pour charger les éléments depuis l'API
        async void OnAppearing()
        {

            await LoadItemsEvent();
        }

        [RelayCommand]
        async Task LoadItemsEvent()
        {

            IsBusy = true;
            try
            {
                ObsItemsEvents.Clear();
                //DateTime selectedDate = new DateTime(2024, 10, 01);
                //var items = await _EventApi.GetEventByDateAsync(selectedDate);                 //Commenter ligne 35 et 36 pour par date et 39 pour All


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
                    await Application.Current.MainPage.DisplayAlert("Information", "Aucun événement trouvé pour cette date.", "OK");
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
                ObsItemsEvents.Clear();
                //DateTime selectedDate = new DateTime(2024, 10, 01);
                //var items = await _EventApi.GetEventByDateAsync(selectedDate);                 //Commenter ligne 35 et 36 pour par date et 39 pour All


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

    }
}
