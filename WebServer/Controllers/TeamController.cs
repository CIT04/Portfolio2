using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/team")]
[ApiController]
public class TeamController : BaseController
{
    private readonly ITeamService _dataService;

    public TeamController(ITeamService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetTeams))]

    public IActionResult GetTeams([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var teams, var total) = _dataService.GetTeams(searchParams.page, searchParams.pageSize);

        var items = teams.Select(CreateTeamModel);

        var result = Paging(items, total, searchParams, nameof(GetTeams));

        return Ok(result);
    }

    [HttpGet("{MediaId}", Name = nameof(GetTeam))]
    public IActionResult GetTeam(string MediaId)
    {
        var team1 = _dataService.GetTeamByMid(MediaId);

        if (team1 == null)
        {
            return NotFound();
        }

        return Ok(CreateTeamModel(team1));
    }

    public TeamModel CreateTeamModel(Team team)
    {
        return new TeamModel
        {
            Url = GetUrl(nameof(GetTeam), new { team.MediaId }),

            MediaId = team.MediaId,
            PersonId = team.PersonId,
            Role = team.Role,
            Characters = team.Characters,
            Job = team.Job,
        };
    }
}

