using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



public interface IBookmarkService

{
    Bookmark? GetBookmark(string id, int U_id);

    (IList<Bookmark> products, int count) GetBookmarks(int page, int pageSize);

        void CreateBookmark(Bookmark bookmark);
        void DeleteBookmark(int u_id, string m_id);
        bool UpdateBookmark(Bookmark bookmark);
}
