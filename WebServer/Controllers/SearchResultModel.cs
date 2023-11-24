namespace WebServer.Controllers
{
    public class SearchResultModel
    {

        public string Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Rank = {Rank}, Title={Title}";
        }
    }
}