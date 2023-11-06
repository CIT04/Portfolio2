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

    [HttpGet("{Id}", Name = nameof(GetLocalRating))]
    public IActionResult GetLocalRating(string Id)
    {
        var localrating1 = _dataService.GetLocalRating(Id);
        if (localrating1 == null)
        {
            return NotFound();
        }

        return Ok(CreateLocalRatingModel(localrating1));

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

