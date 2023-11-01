

using DataLayer;

var ds = new MediaService();

var m = ds.GetMedia("tt0098936");

Console.WriteLine($"{m.Title}, {string.Join(", ", m.MediaGenres.Select(x => x.GenreId))}");