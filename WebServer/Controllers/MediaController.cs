using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/media")]
[ApiController]
public class MediaController : BaseController
{
    private readonly IMediaService _dataService;

    public MediaController(IMediaService dataService, LinkGenerator linkGenerator)
        :base(linkGenerator)
    {
        _dataService = dataService;

    }

    
    [HttpGet(Name = nameof(GetMedias))]
    public IActionResult GetMedias([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var medias, var total) = _dataService.GetMedias(searchParams.page, searchParams.pageSize);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, searchParams, nameof(GetMedias));

        return Ok(result);
    }


    [HttpGet("{id}", Name = nameof(GetMedia))]
    public IActionResult GetMedia(string id)
    {
        var media = _dataService.GetMedia(id);
        if (media == null)
        {
            return NotFound();
        }

        return Ok(CreateMediaModel(media));
    }


    [HttpGet("genre/{genre}", Name = nameof(GetMediasByGenre))]
    public IActionResult GetMediasByGenre([FromQuery] SearchParams searchParams, [FromRoute]string genre = null)
    {
        UpdateSearchParamsFromQuery(searchParams);
        searchParams.Genre = genre;

        (var medias, var total) = _dataService.GetMediasByGenre(searchParams.page, searchParams.pageSize, searchParams.Genre);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, searchParams, nameof(GetMediasByGenre));

        return Ok(result);
    }

    [HttpGet("type/{type}", Name = nameof(GetMediasByType))]
    public IActionResult GetMediasByType([FromQuery] SearchParams searchParams, [FromRoute] string type = null)
    {
        UpdateSearchParamsFromQuery(searchParams);
        searchParams.Type= type;

        (var medias, var total) = _dataService.GetMediasByType(searchParams.page, searchParams.pageSize, searchParams.Type);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, searchParams, nameof(GetMediasByType));

        return Ok(result);
    }

    [HttpGet("search", Name = nameof(GetMediasBySearch))]
    public IActionResult GetMediasBySearch([FromQuery] SearchParams searchParams, [FromQuery] string search = null) 
    {
       // search = "Harry potter";
        UpdateSearchParamsFromQuery(searchParams);
        searchParams.search = search;

        (var medias, var total) = _dataService.GetMediasBySearch(searchParams.page, searchParams.pageSize, searchParams.search);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, searchParams, nameof(GetMediasBySearch));

        return Ok(result);
    }

    
    //public IActionResult Search([FromQuery] SearchParams searchParams, [FromRoute] string type = null, [FromRoute] string genre = null, [FromRoute] string [] search = null)
    //{ 
    //    UpdateSearchParamsFromQuery(searchParams);
    //    searchParams.Type = type;
    //    searchParams.Genre = genre;
    //    searchParams.Search = search;

    //    (var medias, var total) = _dataService.Search(searchParams.page, searchParams.pageSize, searchParams.Search ,searchParams.Type, searchParams.Genre);

    //    var items = medias.Select(CreateMediaModel);

    //   //  var result = Paging(items, total, searchParams, nameof(GetMediasByType));

    //     //return Ok(result);
    //    return Ok(items);

    //}
    private MediaModel CreateMediaModel(MediaDTO media)
    {
        return new MediaModel
        {
            Url = GetUrl(nameof(GetMedias), new { media.Id }),
            Title = media.Title,
            Year = media.Year,
            Plot = media.Plot,
            Released = media.Released,
            Poster = media.Poster,
            Runtime = media.Runtime,
            IsAdult = media.IsAdult,
            EndYear = media.EndYear,
            Rated = media.Rated,
            Awards = media.Awards,
            MediaGenres = media.MediaGenres.Select(x => x.GenreId).ToList(),
            MediaCountries = media.MediaCountries.Select(x => x.CountryId).ToList(),
            MediaLanguages = media.MediaLanguages.Select(x => x.LanguageId).ToList(),
            Rating = media.Rating,
            Type = media.Type, 
            Episode = media.Episode,
            TotalSeasons = media.TotalSeasons,
            

        };

        
    }
    private MediaModel CreateMediaModel(Media media)
    {
        return new MediaModel
        {
            Url = GetUrl(nameof(GetMedias), new { media.Id }),
            Title = media.Title,
            Year = media.Year,
            Plot = media.Plot,
            Released = media.Released,
            Poster = media.Poster,
            Runtime = media.Runtime,
            IsAdult = media.IsAdult,
            EndYear = media.EndYear,
            Rated = media.Rated,
            Awards = media.Awards,
            MediaGenres = media.MediaGenres.Select(x => x.GenreId).ToList(),
            MediaCountries = media.MediaCountries.Select(x => x.CountryId).ToList(),
            MediaLanguages = media.MediaLanguages.Select(x => x.LanguageId).ToList(),
            Type = media.Type,
            



        };


    }











}
