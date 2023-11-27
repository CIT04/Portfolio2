using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorController : BaseController
{
    private readonly IActorService _actordataService;
    private readonly IMediaService _mediadataService;
    public ActorController(IActorService actorDataService,IMediaService mediaDataService, LinkGenerator linkGenerator)
       : base(linkGenerator)
    {
        _actordataService = actorDataService;
        _mediadataService = mediaDataService;

    }
   
    [HttpGet(Name = nameof(GetActors))]
    public IActionResult GetActors([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var actors, var total) = _actordataService.GetActors(searchParams.page, searchParams.pageSize);

        var items = actors.Select(CreateActorModel);

        var result = Paging(items, total, searchParams, nameof(GetActors));

        return Ok(result);
       
    }
    // [HttpGet("{m_id}", Name = nameof(GetMediaActors))]
    // public IActionResult GetMediaActors(string m_id, [FromQuery] SearchParams searchParams)
    // {
    //     UpdateSearchParamsFromQuery(searchParams);
    //     var actorsDTO = _actordataService.GetActorsForMedia(searchParams.page, searchParams.pageSize, m_id);

    //     var actorModels = actorsDTO.Select(dto => new ActorModel
    //     {
    //         // Map the properties from the DTO to the ActorModel
    //         Id = dto.Id,
    //         Name = dto.Name,
    //         // You can map other properties as needed
    //     });

    //  var result = Paging(actorModels, 0, searchParams, nameof(_mediadataService.GetMedias));

    //  return Ok(result);
          
    //}

    [HttpGet("{p_id}", Name = nameof(GetActorsObject))]
    public IActionResult GetActorsObject(string p_id, [FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        var actorsDTO2 = _actordataService.GetActorsObject(searchParams.page, searchParams.pageSize, p_id);

        var actorModels = actorsDTO2.Select(dto => new ActorModel
        {

            Id = dto.Id,
            Name = dto.Name,
            Birthyear = dto.Birthyear,
            KnownForTitles = dto.KnownForTitles

        });

        var result = Paging(actorModels, 0, searchParams, nameof(_mediadataService.GetMedias));

        return Ok(result);

    }
    private ActorModel CreateActorModel(Person actor)
    {
        return new ActorModel
        {
            Id = actor.Id,
            Name = actor.Name,
            Birthyear = actor.BirthYear,
            KnownForTitles = actor.KnownForTitles

        };
    }


}
