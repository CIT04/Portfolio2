using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;
using DataLayer.Objects;


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

    public User? GetUser(string id)
    {
        var db = new NorthwindContex();
        return db.User.FirstOrDefault(x => x.Id == id);
    }


    /*------------SeasonEpisode--------------*/
    public (IList<SeasonEpisode> products, int count) GetSeasonEpisodes(int page, int pageSize)
    {
        var db = new NorthwindContex();
        var se =
            db.SeasonEpisode
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (se, db.SeasonEpisode.Count());
    }

    public SeasonEpisode? GetSeasonEpisode(string id)
    {
        var db = new NorthwindContex();
        return db.SeasonEpisode.FirstOrDefault(x => x.M_id == id);

    }

        public Country? GetCountry(string country)
        {
            var db = new NorthwindContex();
            return db.Country.FirstOrDefault(x => x.country == country);

        }

        public (IList<Country> products, int count) GetCountries(int page, int pageSize)
        {
            var db = new NorthwindContex();
            var country =
                db.Country
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return (country, db.Country.Count());
        }
    }


