using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
//TODO: Try/Catch exception, and write tests for invalid input for ALL methods

public class BookmarkService : IBookmarkService
{
    public IEnumerable<Bookmark> GetBookmarks(int U_id)
    {
        var db = new Context();
        return db.Bookmarks.Where(x => x.U_id == U_id).ToList();

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


    //CRUD Create
    public void CreateBookmark(Bookmark bookmark)
    {
        using var db = new Context();
        var M_id = db.Bookmarks.Max(x => x.M_id) + 1;
        var xBookmark = new Bookmark
        { 
            M_id = bookmark.M_id,
            U_id = bookmark.U_id,
            Time = bookmark.Time
           
        };

        db.Database.ExecuteSqlInterpolated($"select insert_bookmark({xBookmark.M_id}, {xBookmark.U_id})");

        db.SaveChanges();
    }


    //CRUD Update
    public bool UpdateBookmark(Bookmark bookmark)
    {
        using var db = new Context();
        var xBookmark = db.Bookmarks.FirstOrDefault(x => x.M_id == bookmark.M_id && x.U_id == bookmark.U_id);

        if (xBookmark != null) 
        {
            db.Database.ExecuteSqlInterpolated($"select update_bookmark({bookmark.M_id}, {bookmark.U_id})");
            db.SaveChanges();
            return true;
        }
        return false;
    }


    //CRUD Delete 
    public void DeleteBookmark(Bookmark bookmark) 
        {
            using var db = new Context();
            db.Database.ExecuteSqlInterpolated($"select delete_bookmark_by_mid({bookmark.M_id},{bookmark.U_id})");
            db.SaveChanges();
        }


}