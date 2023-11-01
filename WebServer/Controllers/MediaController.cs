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
    public IActionResult GetMedias(SearchParams searchParams)
    {
        (var medias, var total) = _dataService.GetMedias(searchParams.page, searchParams.pageSize);

        var items = medias.Select(CreateMediaModel);

        var result = PagingDelux(items, total, searchParams, nameof(GetMedias));

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
        searchParams.Genre = genre;
        (var medias, var total) = _dataService.GetMediasByGenre(searchParams.page, searchParams.pageSize, searchParams.Genre);

        var items = medias.Select(CreateMediaModel);

        var result = PagingDelux(items, total, searchParams, nameof(GetMediasByGenre));

        return Ok(result);
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
            MediaLanguages = media.MediaLanguages.Select(x => x.LanguageId).ToList()

        };

        
    }
   

    








}
