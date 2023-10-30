using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;
using DataLayer.Objects;


public class MediaService : IMediaService
{

    public (IList<Media> products, int count) GetMedias(int page, int pageSize)
    {
        var db = new Context();
        var media =
            db.Media
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (media, db.Media.Count());
    }
    public IList<Media> GetMediasByTitle(string search)
    {
        var db = new Context();
        return db.Media.Where(x => x.Title.ToLower().Contains(search.ToLower())).ToList();
    }

    public Media? GetMedia(string id)
    {
        var db = new Context();
        return db.Media.FirstOrDefault(x => x.Id == id);
    }

    /*--------User------------*/



    /*------------SeasonEpisode--------------*/
    public (IList<SeasonEpisode> products, int count) GetSeasonEpisodes(int page, int pageSize)
    {
        var db = new Context();
        var se =
            db.SeasonEpisode
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (se, db.SeasonEpisode.Count());
    }

    public SeasonEpisode? GetSeasonEpisode(string id)
    {
        var db = new Context();
        return db.SeasonEpisode.FirstOrDefault(x => x.M_id == id);

    }
    /*------------Country--------------*/

    public Country? GetCountry(string country)
        {
            var db = new Context();
            return db.Country.FirstOrDefault(x => x.country == country);

        }

        public (IList<Country> products, int count) GetCountries(int page, int pageSize)
        {
            var db = new Context();
            var country =
                db.Country
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return (country, db.Country.Count());
        }

 
}


