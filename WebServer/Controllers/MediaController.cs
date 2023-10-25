using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/media")]
[ApiController]
public class MediaController : BaseController
{
    private readonly IDataService _dataService;

    public MediaController(IDataService dataService, LinkGenerator linkGenerator)
        :base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetMedias))]
    public IActionResult GetMedias(int page = 0, int pageSize = 10)
    {
        (var medias, var total) = _dataService.GetMedias(page, pageSize);

        var items = medias.Select(CreateMediaModel);

        var result = Paging(items, total, page, pageSize, nameof(GetMedias));

        return Ok(result);
    }
    private MediaModel CreateMediaModel(Media media)
    {
        return new MediaModel
        {
            Url = GetUrl(nameof(GetMedias), new { media.Id }),
            Title = media.Title,
            
        };
    }

}
