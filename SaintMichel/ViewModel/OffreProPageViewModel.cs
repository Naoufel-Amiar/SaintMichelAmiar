using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Views;
using SaintMichel.Model;
namespace SaintMichel.ViewModel
{
    public partial class OffreProPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<OffrePro> _obsItems; // Les items à afficher dans le ListView

        OffrePro_API _API;

        public OffreProPageViewModel(OffrePro_API api)
        {
            _API = api;
            ObsItems = new ObservableCollection<OffrePro>(); // Initialisation de la collection Observable
            OnAppearing();
        }

        // Méthode pour charger les éléments depuis l'API
        async void OnAppearing()
        {

            await LoadItems();
        }

        [RelayCommand   ]
        async Task LoadItems()
        {
            IsBusy = true;
            try
            {
                ObsItems.Clear();
                var items = await _API.GetOffreProAsync(); // Appel API pour récupérer les données

                foreach (var item in items)
                {
                    ObsItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur: {ex.Message}"); // Afficher les erreurs dans la console
                await Application.Current.MainPage.DisplayAlert("Erreur", "Une erreur est survenue.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //[RelayCommand]
        //async void OffreTapped(OffrePro items)
        //{
        //    if (items == null)
        //    {

        //        return;

        //    }
        //    await Shell.Current.GoToAsync($"{nameof(OffreProDetailPage)}?{nameof(OffreProDetailPageViewModel.itemId)}={items.IDInterim}");
        //}
    }
}
