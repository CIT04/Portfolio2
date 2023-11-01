using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class MediaCountry
    {
        public string MediaId { get; set; }
        public string CountryId { get; set; }

        public Media Media { get; set; }
        public Country Country { get; set; }

    }
}
