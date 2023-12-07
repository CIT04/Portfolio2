using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



public interface IBookmarkService

{
    IEnumerable<Bookmark>? GetBookmarks(int U_id);

    (IList<Bookmark> products, int count) GetBookmarks(int page, int pageSize);

        void CreateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
        bool UpdateBookmark(Bookmark bookmark);
}
