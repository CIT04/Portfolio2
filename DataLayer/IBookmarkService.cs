using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



public interface IBookmarkService

{
    Bookmark? GetBookmark(string id);

    (IList<Bookmark> products, int count) GetBookmarks(int page, int pageSize);
}
