using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;
using DataLayer.Objects;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


public class MediaService : IMediaService
{

    public (IList<Media> products, int count) GetMedias(int page, int pageSize)
    {
        var db = new Context();
        var media =
            db.Media
            .Include(m => m.MediaGenres)
            .Include(c => c.MediaCountries)
            .Include(l => l.MediaLanguages)
           // .Include(v => v.Rating)
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
    public (IList<Media> products, int count) GetMediasByGenre(int page, int pageSize, string search)
    {

        using (var db = new Context())
        {
            var query = db.Media
                .Include(m => m.MediaGenres)
                .Include(c => c.MediaCountries)
                .Include(l => l.MediaLanguages)
                .Where(m => m.MediaGenres.Any(g => g.Genre.Id.ToLower().Contains(search.ToLower())));

            var media = query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return (media, query.Count());
        }
    }
    public (IList<Media> products, int count) GetMediasByType(int page, int pageSize, string search)
    {
        var db = new Context();
        var media =
            db.Media
            .Include(m => m.MediaGenres)
            .Include(c => c.MediaCountries)
            .Include(l => l.MediaLanguages)
            .Where(m => m.Type.ToLower().Contains(search.ToLower()));
        var result = media
            .Skip(page * pageSize)
            .Take(pageSize)

            .ToList();
        return (result, media.Count());
    }

    public MediaDTO? GetMedia(string id)
    {
        var db = new Context();
        var media = db.Media
            .Include(m => m.MediaGenres)
            .Include(c => c.MediaCountries)
            .Include(l => l.MediaLanguages)
            
            .FirstOrDefault(x => x.Id == id);

       

        //TODO: Fix mapping issues
        if (media != null)
        {

            var dto = new MediaDTO
            {
                Title = media.Title,
                Year = media.Year,
                Plot = media.Plot,
                Released = media.Released,
                Poster = media.Poster,
                Runtime = media.Runtime,
                IsAdult = media.IsAdult,
                EndYear = media.EndYear,
                Rated = media.Rated,
                Awards = media.Awards,
                MediaGenres = media.MediaGenres,
                MediaCountries = media.MediaCountries,
                MediaLanguages = media.MediaLanguages,
                //SeasonEpisode = media.SeasonEpisode
                
    };
            dto.Rating = db.Rating.FirstOrDefault(x => x.Id == id);
            dto.TotalSeasons = db.SeasonEpisode.FirstOrDefault(x => x.Id == id).TotalSeasons;
            dto.Episode = db.SeasonEpisode.FirstOrDefault(x => x.Id == id).Episode;
            
            return dto;

        }
        return null;
    }

    /*--------User------------*/
    //Rykket til UserService


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
        return db.SeasonEpisode.FirstOrDefault(x => x.Id == id);

    }

    public Rating? getrating(string id)
    {
        var db = new Context();
        return db.Rating.FirstOrDefault(x => x.Id == id);

    }


}


