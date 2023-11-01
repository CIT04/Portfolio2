using DataLayer.Objects;

namespace WebServer.Models;

public class MediaModel
{
    public string Url { get; set; } = string.Empty;
   
    public string Title { get; set; } = string.Empty;

    public string Year { get; set; }

    public string Plot { get; set; }

    public string Released { get; set; }

    public string Poster { get; set; }


    public string? Runtime { get; set;}

    public bool IsAdult { get; set; }

    public string EndYear { get; set; }
    public string Rated { get; set; }
    public string Awards { get; set; }

    public string Average { get; set; }

    public List<String> MediaGenres { get; set; }
    public List<String> MediaCountries { get; set; }

}
