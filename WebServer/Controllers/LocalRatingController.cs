using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

    //TODO: Add authorization for - Administrators 
    [HttpGet(Name = nameof(GetLocalRatings))]
    public IActionResult GetLocalRatings([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var localratings, var total) = _dataService.GetLocalRatings(searchParams.page, searchParams.pageSize);

        var items = localratings.Select(CreateLocalRatingModel);

        var result = Paging(items, total, searchParams, nameof(GetLocalRatings));

        return Ok(result);
    }

    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpGet("mid/{m_id}", Name = nameof(GetLocalRating))]
    public IActionResult GetLocalRating(string Id)
    {
        var localrating1 = _dataService.GetLocalRating(Id);
        if (localrating1 == null)
        {
            return NotFound();
        }

        return Ok(CreateLocalRatingModel(localrating1));

    }
    //TODO: Add authorization for - Administrators AND for the requesting users Id
  
    [HttpGet("{u_id}", Name = nameof(GetLocalRatingByUid))]
    public IActionResult GetLocalRatingByUid(int u_id)
    {
        var localratings = _dataService.GetLocalRatingByUid(u_id);

        if (localratings == null || !localratings.Any())
        {
            return NotFound();
        }

        var localRatingModels = localratings.Select(CreateLocalRatingModel);

        return Ok(localRatingModels);
    }

    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpPost("create")]
    public IActionResult CreateLocalRating(LocalRating localRating)
    {
        try
        {
            using var db = new Context();
            var xLocalRating = new LocalRating
            {
                M_id = localRating.M_id,
                U_id = localRating.U_id,
                LocalScore = localRating.LocalScore
            };

            db.Database.ExecuteSqlInterpolated($"SELECT rate({xLocalRating.M_id}, {xLocalRating.U_id}, {xLocalRating.LocalScore})");

            db.SaveChanges();

            // Return ok msg when created sucessfully
            return Ok("Rating Created");
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.SqlState == "P0001") // take the exception raised in sql function
            {
                
                return BadRequest("Error: " + ex.Message);
            }
            // if the m_id/u_id does not exist return this error
            return StatusCode(500, "User or movie does not exist, please check your input and try again.");
        }
    }



    //TODO: Update path? 
    //TODO: Add authorization for - Administrators AND for the requesting users Id
    //CRUD Update
    [HttpPut("update")]
    public IActionResult UpdateLocalRating(LocalRating localrating)
    {

        var result=_dataService.UpdateLocalRating(localrating);
        if (result) 
        { return Ok(localrating); }
        return NotFound();
    }

    //TODO: Add authorization for - Administrators AND for the requesting users Id
    //TODO: u_id/m_id path does not look good in url, needs fix
    //CRUD Delete
    [HttpDelete("delete")]
    public IActionResult DeleteLocalRating(LocalRating localrating)
    {
        
        if (localrating == null)
        {
            return NotFound();
        }
        _dataService.DeleteLocalRating(localrating);
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

