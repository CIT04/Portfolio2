using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class SearchResult
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return $"Id = {Id}, Rank = {Rank}, Title={Title}";
        }
    }
}
