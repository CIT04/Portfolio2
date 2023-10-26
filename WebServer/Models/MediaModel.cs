namespace WebServer.Models;

public class MediaModel
{
    public string Url { get; set; } = string.Empty;
   
    public string Title { get; set; } = string.Empty;

    public string Year { get; set; }

    public string Plot { get; set; }

    public string Released { get; set; }

    public string Poster { get; set; }

<<<<<<< Updated upstream
    public string Runtime { get; set;}

    public bool IsAdult { get; set; }
=======
    public string EndYear { get; set; }
>>>>>>> Stashed changes
}
