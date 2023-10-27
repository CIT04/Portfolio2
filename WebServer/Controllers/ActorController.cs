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
    public IActionResult GetActors(int page = 0, int pageSize = 10)
    {
        (var actors, var total) = _actordataService.GetActors(page, pageSize);

        var items = actors.Select(CreateActorModel);

        var result = Paging(items, total, page, pageSize, nameof(GetActors));

        return Ok(result);
    }
    [HttpGet("{m_id}", Name = nameof(GetMediaActors))]
    public IActionResult GetMediaActors(string m_id)
    {
        var actorsDTO = _actordataService.GetActorsForMedia(m_id);

        var actorModels = actorsDTO.Select(dto => new ActorModel
        {
            // Map the properties from the DTO to the ActorModel
            Id = dto.Id,
            Name = dto.Name,
            // You can map other properties as needed
        });

        return Ok(actorModels);
    }

    private ActorModel CreateActorModel(Person actor)
    {
        return new ActorModel
        {
            Id = actor.Id,
            Name = actor.Name

        };
    }


}
