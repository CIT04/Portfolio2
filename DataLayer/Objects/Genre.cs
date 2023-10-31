using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class Genre
    {
        public string Id { get; set; }

        public ICollection<MediaGenre> MediaGenres { get; set; }
    }
}
