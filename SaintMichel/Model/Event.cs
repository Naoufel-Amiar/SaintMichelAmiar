using System.Text.Json.Serialization;

namespace SaintMichel.Model
{
    public class Event
    {
        public int IDevent { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Lieu { get; set; }
        public int state { get; set; }
        public int user_iduser { get; set; }



        [JsonIgnore]
        public string FormattedDate
        {
            get
            {
                if (DateTime.TryParse(Date, out DateTime parsed))
                    return parsed.ToString("dd/MM/yyyy à HH:mm");
                else
                    return Date;
            }
        }

        [JsonIgnore]
        public string PostInfo
        {
            get
            {
                if (DateTime.TryParse(Date, out DateTime parsed))
                    return $"Événement posté le {parsed:dd/MM/yyyy} à {parsed:HH:mm}";
                else
                    return $"Événement posté à une date inconnue";
            }
        }

    }

} 
