using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public IActionResult GetMedias(int page = 0, int pageSize = 10)
    {
        (var medias, var total) = _dataService.GetMedias(page, pageSize);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, page, pageSize, nameof(GetMedias));

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
            Awards= media.Awards,

        };
    }
   

    //--------------Genre--------------//
    [HttpGet(Name = nameof(GetGenres))]
    public IActionResult GetGenres(int page = 0, int pageSize = 10)
    {
        (var genres, var total) = _dataService.GetGenres(page, pageSize);

        var items = genres.Select(CreateGenreModel);

        var result = Paging(items, total, page, pageSize, nameof(GetGenres));

        return Ok(result);
    }

    [HttpGet("{genre}", Name = nameof(GetGenre))]
    public IActionResult GetGenre(string genre)
    {
        var genre1 = _dataService.GetGenre(genre);
        if (genre1 == null)
        {
            return NotFound();
        }

        return Ok(CreateGenreModel(genre1));

    }

    public GenreModel CreateGenreModel(Genre genre)
    {
        return new GenreModel
        {
            Url = GetUrl(nameof(GetGenres), new {genre.genre}),
            Genre = (string)genre.genre,
        };
    }



















}
