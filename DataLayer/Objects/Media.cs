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

        public string Runtime { get; set; }

        ////Måske ikke bool?
        public bool IsAdult { get; set; }

       

        //Magnler - Awards, DVD,Production,Boxoffice


        public string? Boxoffice { get; set; }


    }
}

//Making a user class
public class User
{
    [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}