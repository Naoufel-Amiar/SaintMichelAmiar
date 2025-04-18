namespace SaintMichel.Model
{
    public class User
    {
        public int iduser { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string password { get; set; }
        public string date_naissance { get; set; }
        public string photo_profil { get; set; }
        public string adresse_postal { get; set; }
        public string role { get; set; }
        public double rating { get; set; }
        public string pseudo { get; set; }
        public string Rfid_carte { get; set; }
        public List<string> experiences { get; set; }
        public List<string> formations { get; set; }
        public List<string> competences { get; set; }
        public List<string> langues { get; set; }
        public List<string> centre_interets { get; set; }
    }
}
