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

    public IActionResult GetRatings(int page = 0, int pageSize = 10)
    {
        (var ratings, var total) = _dataService.GetRatings(page, pageSize);

        var items = ratings.Select(CreateRatingModel);

        var result = Paging(items, total, page, pageSize, nameof(GetRatings));

        return Ok(result);
    }

    [HttpGet("{rating}", Name = nameof(GetRating))]
    public IActionResult GetRating(string rating)
    {
        var rating1 = _dataService.GetRating(rating);
        if (rating1 == null)
        {
            return NotFound();
        }

        return Ok(CreateRatingModel(rating1));

    }

    public RatingModel CreateRatingModel(Rating rating)
    {
        return new RatingModel
        {
            Url = GetUrl(nameof(GetRatings), new { rating.Ratings }),

            Rating = (string)rating.Ratings,
            M_id = rating.M_id,
            ImdbRatings = rating.ImdbRatings,
            ImdbVotes = rating.ImdbVotes,
            AverageRating = rating.AverageRating,
            NumVotes = rating.NumVotes,

        };




    }
}

