using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Objects
{
    public class SeasonEpisode
    {
        public string Id { get; set; } = string.Empty;
        public string Episode { get; set; }

       // public string SeasonNumber { get; set; }

        public string TotalSeasons { get; set;}
    }
}

//Making a user class
