using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.Extensions.Configuration;
var ds = new LocalRatingService();
var locals = ds.GetLocalRatingByUid(1);


//Console.WriteLine(results);

foreach (var local in locals)
{
    Console.WriteLine(local.U_id + "-" + "+" + local.M_id + "-" + local.LocalScore);
}

//var mediaList = ds.GetMediasByGenre(1,10,"Horror" );
//var db = new Context(); // Replace "YourContext" with your actual context class name

//string[] search = new[] { "peter","parker","spider" };

//var results = db.SearchResult.FromSqlInterpolated($"SELECT * FROM search_media({(search)})");


//foreach (var result in results)
//{
//    Console.WriteLine(result); 
//}


//var m = ds.GetMedia("tt0098936");

//var medialist = ds.Search(0,10, "Harry", "","" ).products;


//foreach (var media in medialist)
//{
//    Console.WriteLine($"{medialist.ToString}, {media.Title}");
//}

//Console.WriteLine($"{m.Title}"
//$"{string.Join(", ", m.MediaGenres.Select(x => x.GenreId))}," +, 
// $"{string.Join(", ", m.Rating.Ratings.Select(x => x.Id))}"
