using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/localrating")]
[ApiController]
public class LocalRatingController : BaseController
{
    private readonly ILocalRatingService _dataService;

    public LocalRatingController(ILocalRatingService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetLocalRatings))]

    public IActionResult GetLocalRatings([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var localratings, var total) = _dataService.GetLocalRatings(searchParams.page, searchParams.pageSize);

        var items = localratings.Select(CreateLocalRatingModel);

        var result = Paging(items, total, searchParams, nameof(GetLocalRatings));

        return Ok(result);
    }

    [HttpGet("{localrating}", Name = nameof(GetLocalRating))]
    public IActionResult GetLocalRating(string Id)
    {
        var localrating1 = _dataService.GetLocalRating(Id);
        if (localrating1 == null)
        {
            return NotFound();
        }

        return Ok(CreateLocalRatingModel(localrating1));

    }

    //CRUD Create 
    [HttpPost]
    public IActionResult CreateLocal(CreateLocalRatingModel localrating)
    {
        var xLocalRating = new LocalRating
        {
            M_id = localrating.m_id,
            U_id = localrating.u_id,
            LocalScore = localrating.localscore
        };

        _dataService.CreateLocalRating(xLocalRating);

        return Created($"api/localrating/{xLocalRating.M_id}", xLocalRating);
    }

    //CRUD Update
    [HttpPut("update")]
    public IActionResult UpdateLocalRating(LocalRating localrating)
    {

        var result=_dataService.UpdateLocalRating(localrating);
        if (result) 
        { return Ok(localrating); }
        return NotFound();
    }

    //CRUD Delete
    [HttpDelete("{u_id}/{m_id}")]
    public IActionResult DeleteLocalRating(int u_id, string m_id)
    {
        var localrating = _dataService.GetLocalRating(m_id);
        if (localrating == null)
        {
            return NotFound();
        }
        _dataService.DeleteLocalRating(u_id, m_id);
        return Ok("LocalRating Deleted");
    }

    public LocalRatingModel CreateLocalRatingModel(LocalRating localrating)
    {
        return new LocalRatingModel
        {
            Url = GetUrl(nameof(GetLocalRatings), new { localrating.M_id, localrating.U_id }),

            M_id = localrating.M_id,
            U_id = localrating.U_id,
            LocalScore = localrating.LocalScore,

        };
    }
}

