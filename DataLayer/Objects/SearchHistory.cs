using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class SearchHistory
    {
       
           [Key]
            public int U_id { get; set; }
            public string Search_string { get; set; }
            public string Time { get; set; }
        
    }

}

