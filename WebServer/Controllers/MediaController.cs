 using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/media")]
[ApiController]
public class MediaController : BaseController
{
    private readonly IMediaService _dataService;
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public MediaController(IMediaService dataService, IUserService userService, LinkGenerator linkGenerator, IConfiguration configuration)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _userService = userService;
        _configuration = configuration;
    }


    [HttpGet(Name = nameof(GetMedias))]
    public IActionResult GetMedias([FromQuery] SearchParams searchParams)
    {   
        

         
         
            UpdateSearchParamsFromQuery(searchParams);

            (var medias, var total) = _dataService.GetMedias( searchParams.page, searchParams.pageSize);

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
    public IActionResult GetMediasByGenre([FromQuery] SearchParams searchParams, [FromRoute] string genre = null)
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
        searchParams.Type = type;

        (var medias, var total) = _dataService.GetMediasByType(searchParams.page, searchParams.pageSize, searchParams.Type);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, searchParams, nameof(GetMediasByType));

        return Ok(result);
    }

    [HttpGet("search", Name = nameof(GetMediasBySearch))]
    public IActionResult GetMediasBySearch([FromQuery] SearchParams searchParams, [FromQuery] string search = null, [FromQuery] string type = null, [FromQuery] int? u_id=null)
    {
        // search = "Harry potter";
        UpdateSearchParamsFromQuery(searchParams);
        searchParams.search = search;
        searchParams.Type = type;
        searchParams.u_id=u_id;


        (var searchResults, var total) = _dataService.GetMediasBySearch(searchParams.page, searchParams.pageSize, searchParams.search, searchParams.Type, searchParams.u_id);

        var items = CreateSearchResultModel(searchResults);

        var result = Paging(items, total, searchParams, nameof(GetMediasBySearch));

        return Ok(result);
    }

    [HttpGet("team/{m_id}", Name = nameof(GetTeamForMedia))]
    public IActionResult GetTeamForMedia(string m_id)
    {
        var teams = _dataService.GetActorsForMedia(m_id);
        IList<Team> actors = teams.actors;  
        IList<Team> directors = teams.writersanddirectors;
        IList<Team> crew = teams.crew;

        var teamModel = CreateTeamForMediaModel(actors, directors, crew);

        return Ok(teamModel);
    }

    internal TeamForMediaModel CreateTeamForMediaModel(IList<Team> actor, IList<Team> writersanddirector, IList<Team> crew)
    {
        return new TeamForMediaModel
        {
            Actor = actor,
            WritersAndDirectors = writersanddirector,
            Crew = crew
        };


    }



    private IList<SearchResultModel> CreateSearchResultModel(IList<SearchResult> searchResults)
    {
        return searchResults.Select(searchResult => new SearchResultModel
        {
            Id = searchResult.Id,
            Rank = searchResult.Rank,
            Title = searchResult.Title,
            Poster=searchResult.Poster,
            Rating=searchResult.Rating,
            Year=searchResult.Year,
            Path = GetUrl(nameof(GetMedias), new { searchResult.Id })
        }).ToList();
    }


    private MediaModel CreateMediaModel(MediaDTO media)
    {
        return new MediaModel
        {
            Id = media.Id,
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
            Id = media.Id,
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
