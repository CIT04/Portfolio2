using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class MediaGenre
    {
        public string m_id { get; set; }
        public string genreid { get; set;}


        public Genre genre { get; set; }
        public Media media { get; set; }
    }
}
