using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;

public class DataService : IDataService
{
    public (IList<Media> products, int count) GetMedias(int page, int pageSize)
    {
            var db = new NorthwindContex();
            var media =
                db.Media
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return (media, db.Media.Count());
    }
    public IList<Media> GetMediasByTitle(string search)
    {
        var db = new NorthwindContex();
        return db.Media.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
    }

    public Media? GetMedia(string id)
    {
        var db = new NorthwindContex();
        return db.Media.FirstOrDefault(x => x.Id == id);
    }

    /*--------User------------*/


    //public (IList<User> products, int count) GetUsers(int page, int pageSize)
    //{
    //    var db = new NorthwindContex();
    //    var user =
    //        db.Media
    //        .Skip(page * pageSize)
    //        .Take(pageSize)
    //        .ToList();
    //    return (user, db.User.Count());
    //}

}

