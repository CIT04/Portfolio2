using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/rating")]
[ApiController]
public class RatingController : BaseController
{
    private readonly IRatingService _dataService;

    public RatingController(IRatingService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetRatings))]

    public IActionResult GetRatings([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var ratings, var total) = _dataService.GetRatings(searchParams.page, searchParams.pageSize);

        var items = ratings.Select(CreateRatingModel);

        var result = Paging(items, total, searchParams, nameof(GetRatings));

        return Ok(result);
       
    }

    [HttpGet("{m_id}", Name = nameof(GetRating))]
    public IActionResult GetRating(string m_id)
    {
        var rating = _dataService.GetRatingByMId(m_id);

        if (rating == null)
        {
            return NotFound();
        }

        return Ok(CreateRatingModel(rating));
    }

    public RatingModel CreateRatingModel(Rating rating)
    {
        return new RatingModel
        {
            Url = GetUrl(nameof(GetRating), new { m_id = rating.M_id }),

            Rating = rating.Ratings,
            M_id = rating.M_id,
            ImdbRatings = rating.ImdbRatings,
            ImdbVotes = rating.ImdbVotes,
            AverageRating = rating.AverageRating,
            NumVotes = rating.NumVotes,
        };
    }
}

