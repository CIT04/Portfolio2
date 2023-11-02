

using DataLayer;
var ds = new MediaService();
//var mediaList = ds.GetMediasByGenre(1,10,"Horror" );

//foreach (var media in mediaList)
//{
//    Console.WriteLine(media.Title);
//}

var m = ds.GetMedia("tt0098936");

Console.WriteLine($"{m.Title},{m.Rating.Ratings}"
    //$"{string.Join(", ", m.MediaGenres.Select(x => x.GenreId))}," +, 
   // $"{string.Join(", ", m.Rating.Ratings.Select(x => x.Id))}"
    );