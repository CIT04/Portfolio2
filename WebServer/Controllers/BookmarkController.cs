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

    public IActionResult GetBookmarks([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var bookmarks, var total) = _dataService.GetBookmarks(searchParams.page, searchParams.pageSize);

        var items = bookmarks.Select(CreateBookmarkModel);

        var result = Paging(items, total, searchParams, nameof(GetBookmarks));

        return Ok(result);
    }

    [HttpGet("{bookmark}", Name = nameof(GetBookmark))]
    public IActionResult GetBookmark(string bookmark, int U_id)
    {
        var bookmark1 = _dataService.GetBookmark(bookmark, U_id);
        Console.WriteLine("bookmark1");
        if (bookmark1 == null)
        {
            return NotFound();
        }

        return Ok(CreateBookmarkModel(bookmark1));

    }

    //CRUD Create 
    [HttpPost]
    public IActionResult CreateBookmark(CreateBookmarkModel bookmark)
    {
        var xBookmark = new Bookmark
        {
            M_id = bookmark.M_id,
            U_id = bookmark.U_id,
            Time = bookmark.Time,
            Annotation = bookmark.Annotation
        };

        _dataService.CreateBookmark(xBookmark);

        return Created($"api/bookmark/{xBookmark.M_id}", xBookmark);
    }

    //CRUD Update
    [HttpPut("update")]
    public IActionResult UpdateBookmark(Bookmark bookmark)
    {

        var result=_dataService.UpdateBookmark(bookmark);
        if (result) 
        { return Ok(bookmark); }
        return NotFound();
    }

    //CRUD Delete
    [HttpDelete("{M_id, U_id}")]
    public IActionResult DeleteBookmark(string M_id, int U_id)
    {
        var bookmark = _dataService.GetBookmark(M_id, U_id);
        if (bookmark == null)
        {
            return NotFound();
        }
        _dataService.DeleteBookmark(M_id, U_id);
        return Ok("Bookmark Deleted");
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

