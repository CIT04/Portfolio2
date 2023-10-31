namespace WebServer.Models
{
    public class RatingModel
    {
        public string Url { get; set; } = string.Empty;
        public string M_id { get; set; }
        public string ImdbRatings { get; set; }
        public string Rating { get; set; } = string.Empty;
        public string ImdbVotes { get; set; }
        public double AverageRating { get; set; }
        public int NumVotes { get; set; }
    }
}
