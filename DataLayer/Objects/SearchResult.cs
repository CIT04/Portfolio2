namespace DataLayer.Objects
{
    public class SearchResult
    {
        public string Id { get; set; }
        public string Poster { get; set; } 
        public int Rank { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public string Year{ get; set; }


        public override string ToString()
        {
            return $"Id = {Id}, Rank = {Rank}, Title={Title}";
        }
    }
}
