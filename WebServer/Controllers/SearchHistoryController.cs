using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/searchhistory")]
[ApiController]
public class SearchHistoryController : BaseController
{
    private readonly ISearchHistoryService _dataService;

    public SearchHistoryController(ISearchHistoryService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetSearchHistories))]

    public IActionResult GetSearchHistories([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var shistories, var total) = _dataService.GetSearchHistories(searchParams.page, searchParams.pageSize);

        var items = shistories.Select(CreateSearchHistoryModel);

        var result = Paging(items, total, searchParams, nameof(GetSearchHistories));

        return Ok(result);
    }

    [HttpGet("{Id}", Name = nameof(GetSearchHistory))]
    public IActionResult GetSearchHistory(int Id)
    {
        var shistory = _dataService.GetSearchHistory(Id);
        if (shistory == null)
        {
            return NotFound();
        }

        return Ok(CreateSearchHistoryModel(shistory));

    }

    public SearchHistoryModel CreateSearchHistoryModel(SearchHistory searchhistory)
    {
        return new SearchHistoryModel
        {
            Url = GetUrl(nameof(GetSearchHistories), new { searchhistory.U_id }),

            Search_string = searchhistory.Search_string,
            U_id = searchhistory.U_id,
            Time = searchhistory.Time,

        };
    }
    [HttpPost]
    public IActionResult AddSearchHistory(SearchHistoryModel SearchHistoryModel)
    {
        DateTime dateTime = DateTime.Now;

        var searchHistory = new SearchHistory
        {
            U_id = SearchHistoryModel.U_id,
            Search_string = SearchHistoryModel.Search_string,
            Time = dateTime.ToString(),
        };
        _dataService.AddSearchHistory(searchHistory);
        return Created($"api/searchhistory/{searchHistory.U_id}", searchHistory);
    }

    [HttpDelete("{Id}")]

    public IActionResult DeleteSearchHistory(int Id)
    {
        _dataService.DeleteSearchHistory(Id);
        return Ok("Slettet");
        
    }
      
}

