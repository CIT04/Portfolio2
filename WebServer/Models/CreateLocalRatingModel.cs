namespace WebServer.Models
{
    public class CreateLocalRatingModel
    {
        public string Url { get; set; } = string.Empty;
        public string m_id { get; set; }
        public int u_id { get; set; }
        public int localscore { get; set; } 
    }
}
