namespace WebServer.Models
{
    public class CreateBookmarkModel
    {
        public string Url { get; set; } = string.Empty;
        public string M_id { get; set; } 
        public int U_id { get; set; } 
        public string Time { get; set; } 
        public string Annotation { get; set; }
    }
}
