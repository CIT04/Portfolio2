using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class Team
    {
        public string M_id { get; set; }  // Media ID
        public string P_id { get; set; }  // Actor ID

        public Person Person { get; set; }  // Navigation property to Actor

        public Media Media { get; set; }  // Navigation property to Actor

        //public Objects.Media Media { get; set; }  // Navigation property to Media
        //public Objects.Actor Actor { get; set; }  // Navigation property to Actor
    }

}
