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
    //TODO: Add authorization for - Administrators 
    [HttpGet(Name = nameof(GetBookmarks))]

    public IActionResult GetBookmarks([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var bookmarks, var total) = _dataService.GetBookmarks(searchParams.page, searchParams.pageSize);

        var items = bookmarks.Select(CreateBookmarkModel);

        var result = Paging(items, total, searchParams, nameof(GetBookmarks));

        return Ok(result);
    }
    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpGet("{u_id}", Name = nameof(GetBookmark))]
    public IActionResult GetBookmark(int U_id)
    {
        var bookmark1 = _dataService.GetBookmark(U_id);
        Console.WriteLine("bookmark1");
        if (bookmark1 == null)
        {
            return NotFound();
        }

        return Ok(CreateBookmarkModel(bookmark1));

    }

    //CRUD Create 
    //TODO: Add authorization for - Administrators AND for the requesting users Id
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
    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpPut("update")]
    public IActionResult UpdateBookmark(Bookmark bookmark)
    {

        var result=_dataService.UpdateBookmark(bookmark);
        if (result) 
        { return Ok(bookmark); }
        return NotFound();
    }

    //CRUD Delete
    //TODO: Add authorization for - Administrators AND for the requesting users Id
    [HttpDelete("{u_id}/{m_id}")]
    public IActionResult DeleteBookmark(int u_id, string m_id)
    {
    _dataService.DeleteBookmark(u_id, m_id);
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

