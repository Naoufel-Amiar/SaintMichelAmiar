using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SaintMichel.Services
{
    public class Event_API
    {
        public readonly string _baseUrl;

        public Event_API()
        {
            _baseUrl = "http://saintmichel.alwaysdata.net/";
        }



        public async Task<List<Event>> GetEventAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Construire l'URL pour la requête GET
                    string url = $"{_baseUrl}/GetAllEvent";

                    // Effectuer la requête GET
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Lire le contenu de la réponse
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Désérialiser la réponse JSON en une liste d'objets OffrePro
                    List<Event> eventList = JsonConvert.DeserializeObject<List<Event>>(jsonResponse);

                    return eventList;
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs, par exemple journaliser l'exception
                    Console.WriteLine($"Erreur lors de la récupération des offres : {ex.Message}");
                    return null;
                }
            }
        }


        public async Task<List<Event>> GetEventByDateAsync(string date)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    // Construire l'URL pour la requête GET
                    string url = $"{_baseUrl}/GetEventByDate/{date}";

                    // Effectuer la requête GET
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Lire le contenu de la réponse
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Désérialiser la réponse JSON en une liste d'objets OffrePro
                    List<Event> eventList = JsonConvert.DeserializeObject<List<Event>>(jsonResponse);

                    return eventList;
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs, par exemple journaliser l'exception
                    Console.WriteLine($"Erreur lors de la récupération des offres : {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetEventByAreaAsync(string Ville)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Formatage de la date pour l'URL (exemple : "2024-10-01")
                    //string formattedDate = Ville.ToString("yyyy-MM-dd");

                    // Construire l'URL avec la date directement dans le chemin
                    string url = $"{_baseUrl}/GetEventByArea/{Ville}";

                    // Effectuer la requête GET
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Lire le contenu de la réponse
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Désérialiser la réponse JSON en une liste d'objets Event
                    List<Event> eventList = JsonConvert.DeserializeObject<List<Event>>(jsonResponse);

                    return eventList;
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs, par exemple journaliser l'exception
                    Console.WriteLine($"Pas d'Event dans cette Ville =) : {ex.Message}");
                    return null;
                }
            }

        }


        public async Task<Event?> GetEventByIdAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Construire l'URL avec l'ID directement dans le chemin
                    string url = $"{_baseUrl}/GetEventById/{id}";

                    // Effectuer la requête GET
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Lire le contenu de la réponse
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Désérialiser la réponse JSON en un objet Event
                    Event? eventDetails = JsonConvert.DeserializeObject<Event>(jsonResponse);

                    return eventDetails; // Retourner un seul événement
                }
                catch (Exception ex)
                {
                    // Gérer les erreurs
                    Console.WriteLine($"Erreur lors de la récupération de l'événement : {ex.Message}");
                    return null;
                }
            }
        }


    }
}


 