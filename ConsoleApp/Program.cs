﻿using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.Extensions.Configuration;
var ds = new UserService();
var user = ds.GetUserByUsername("Lars2");
Console.WriteLine("userid:"+user.Id);


//var results=ds.GetMediasBySearch(0, 10, search);
//Console.WriteLine(results);

//foreach (var result in results.products)
//{
//    Console.WriteLine($"{result.Id},{result.Title}");
//}

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
