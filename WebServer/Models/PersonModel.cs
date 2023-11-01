namespace WebServer.Models
{
    public class PersonModel
    {
        public string Url { get; set; } = string.Empty;
        public string P_id { get; set; }
        public string PrimaryName { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        public string PrimaryProfession { get; set; }
        public string KnownForTitles { get; set; }
        public string NameRating { get; set; } = string.Empty;
    }
}
