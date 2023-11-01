using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class Person
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string BirthYear { get; set; }

        public string DeathYear { get; set; }

        public string PrimaryProfession { get; set; }

        public string KnownForTitles { get; set; }

        public string? NameRating { get; set; } 

    }
}
