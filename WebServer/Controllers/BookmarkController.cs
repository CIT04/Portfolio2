using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/bookmark")]
[ApiController]
public class BookmarkController : BaseController
{
    private readonly IBookmarkService _dataService;

    public BookmarkController(IBookmarkService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetBookmarks))]

    public IActionResult GetBookmarks(int page = 0, int pageSize = 10)
    {
        (var bookmarks, var total) = _dataService.GetBookmarks(page, pageSize);

        var items = bookmarks.Select(CreateBookmarkModel);

        var result = Paging(items, total, page, pageSize, nameof(GetBookmarks));

        return Ok(result);
    }

    [HttpGet("{bookmark}", Name = nameof(GetBookmark))]
    public IActionResult GetBookmark(string bookmark)
    {
        var bookmark1 = _dataService.GetBookmark(bookmark);
        Console.WriteLine("bookmark1");
        if (bookmark1 == null)
        {
            return NotFound();
        }

        return Ok(CreateBookmarkModel(bookmark1));

    }

    public BookmarkModel CreateBookmarkModel(Bookmark bookmark)
    {
        return new BookmarkModel
        {
            Url = GetUrl(nameof(GetBookmarks), new { bookmark.M_id, bookmark.U_id }),

            //Bookmark = (string)bookmark.Bookmark,
            M_id = bookmark.M_id,
            U_id = bookmark.U_id,
            Time = bookmark.Time,
            Annotation = bookmark.Annotation,

        };




    }
}

