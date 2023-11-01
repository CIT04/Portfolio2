using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class MediaGenre
    {
        public string MediaId {  get; set; }   
        public string GenreId { get; set;}

        public Media Media { get; set; }
        public Genre Genre { get; set; }

    }
}
