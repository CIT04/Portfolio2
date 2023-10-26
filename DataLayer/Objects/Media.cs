using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Objects
{
    public class Media
    {
        public string Id { get; set; }
        public string Title { get; set; }

        //public string OriginalTitle { get; set; }
        public string Year { get; set; }

        public string Plot { get; set; }

        public string Released { get; set; }

        public string Poster { get; set; }

        public string? Runtime { get; set; }

        public bool IsAdult { get; set; }

        public string EndYear {get; set;}

        public string Rated { get; set;}


        //Magnler - Awards, Dvd,  Production,Website, Parrenttconst
        
    }
}

//Making a user class
