using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;
using DataLayer.Objects; 


public class DataService : IDataService
{
   
    public (IList<User> products, int count) GetUsers(int page, int pageSize)
    {
        var db = new NorthwindContex();
        var user =
            db.User
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (user, db.User.Count());
    }

    (IList<Media> products, int count) IDataService.GetMedias(int page, int pageSize)
    {
        var db = new NorthwindContex();
        var media =
            db.Media
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (media, db.Media.Count());
    }

    Media? IDataService.GetMedia(string id)
    {
        var db = new NorthwindContex();
        return db.Media.FirstOrDefault(x => x.Id == id);
    }

    IList<Media> IDataService.GetMediasByTitle(string search)
    {
        var db = new NorthwindContex();
        return db.Media.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
    }

    (IList<User> products, int count) IDataService.GetUsers(int page, int pageSize)
    {
        throw new NotImplementedException();
    }







   

    public IList<ActorsForMediaDTO> GetActorsForMedia(int page, int pageSize, string m_id)
    {
        var db = new NorthwindContex();

        // Use LINQ to query the database
        var actorsForMedia = (from team in db.Team
                              where team.M_id == m_id
                              join person in db.Person on team.P_id equals person.Id
                              select new ActorsForMediaDTO
                              {
                                  Id = person.Id,
                                  Name = person.Name
                              })
                              .Skip(page * pageSize)
                              .Take(pageSize)
                              .ToList();

        return actorsForMedia;

    }
}

