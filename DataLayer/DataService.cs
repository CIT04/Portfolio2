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
}

