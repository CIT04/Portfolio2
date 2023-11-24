using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/history")]
[ApiController]
public class HistoryController : BaseController
{
    private readonly IHistoryService _dataService;

    public HistoryController(IHistoryService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }
    //TODO: Add authorization for - Administrators
    [HttpGet(Name = nameof(GetHistories))]

    public IActionResult GetHistories([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var histories, var total) = _dataService.GetHistories(searchParams.page, searchParams.pageSize);

        var items = histories.Select(CreateHistoryModel);

        var result = Paging(items, total, searchParams, nameof(GetHistories));

        return Ok(result);
    }
    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpGet("{Id}", Name = nameof(GetHistory))]
    public IActionResult GetHistory(string Id)
    {
        var history1 = _dataService.GetHistory(Id);
        if (history1 == null)
        {
            return NotFound();
        }

        return Ok(CreateHistoryModel(history1));

    }

    public HistoryModel CreateHistoryModel(History history)
    {
        return new HistoryModel
        {
            Url = GetUrl(nameof(GetHistories), new { history.M_id, history.U_id }),

            M_id = history.M_id,
            U_id = history.U_id,
            Time = history.Time,

        };
    }
}

