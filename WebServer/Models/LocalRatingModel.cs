namespace WebServer.Models
{
    public class LocalRatingModel
    {
        public string Url { get; set; } = string.Empty;
        public string M_id { get; set; }
        public int U_id { get; set; }
        public int LocalScore { get; set; }
    }
}
