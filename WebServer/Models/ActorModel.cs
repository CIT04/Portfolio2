using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace WebServer.Models;

public class ActorModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; }

    public string Birthyear { get; set; }

    public string KnownForTitles { get; set; }
}
