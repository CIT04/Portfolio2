using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class MediaLanguage
    {
        public string MediaId { get; set; }
        public string LanguageId { get; set; }

        public Media Media { get; set; }
        public Language Language { get; set; }

    }
}
