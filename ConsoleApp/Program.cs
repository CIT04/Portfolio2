

using DataLayer;
var ds = new MediaService();
//var mediaList = ds.GetMediasByGenre(1,10,"Horror" );



//var m = ds.GetMedia("tt0098936");

var medialist = ds.Search(0,10, "Harry", "","" ).products;


foreach (var media in medialist)
{
    Console.WriteLine($"{medialist.ToString}, {media.Title}");
}

//Console.WriteLine($"{m.Title}"
    //$"{string.Join(", ", m.MediaGenres.Select(x => x.GenreId))}," +, 
   // $"{string.Join(", ", m.Rating.Ratings.Select(x => x.Id))}"
    