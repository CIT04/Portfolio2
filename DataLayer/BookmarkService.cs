using DataLayer.Objects;

namespace DataLayer;

public class BookmarkService : IBookmarkService
{
    public Bookmark? GetBookmark(string bookmark)
    {
        var db = new Context();
        return db.Bookmarks.FirstOrDefault(x => x.M_id == bookmark);

    }

    public (IList<Bookmark> products, int count) GetBookmarks(int page, int pageSize)
    {
        var db = new Context();
        var bookmarks =
            db.Bookmarks
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (bookmarks, db.Bookmarks.Count());
    }
}