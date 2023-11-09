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
   
    //TODO: Add exeptions 
    public (IList<Media> products, int count) GetMedias(int page, int pageSize)
    {

        var db = new Context();
       

        var media = GetMediaWithIncludes(db)
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

    //TODO: Try/Catch exception, and write tests for page, pagesize, and invalid input
    public (IList<Media> products, int count) GetMediasByGenre(int page, int pageSize, string search)
    {

        using (var db = new Context())
        {
            var query = GetMediaWithIncludes(db)
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
        using (var db = new Context())
        {
            var query = GetMediaWithIncludes(db)
                .Where(m => m.Type.ToLower().Contains(search.ToLower()));

            var result = query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, query.Count());
        }
    }

    public MediaDTO? GetMedia(int userid,string id)
    {
        using (var db = new Context())
        {
            var user = db.User.FirstOrDefault(x => x.Id == userid);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            var media = GetMediaWithIncludes(db, id);

            if (media != null)
            {
                return MapToMediaDTO(media, db);
            }

            return null;
        }
    }

    public (IList<Media> products, int count) GetMediasBySearch(int page, int pageSize, string search, string type, string genre)
    {
        if (search == null)
        {
            return (null, 0);
        }
        else
        {
            string[] words = search.ToLower().Split(' ');

            using (var db = new Context())
            {
                var searchResult = db.SearchResult.FromSqlInterpolated($"SELECT * FROM search_media({(words)})");
                IQueryable<Media> query = GetMediaWithIncludes(db)
    .Where(media => searchResult.Any(sr => sr.Id == media.Id));

                if (!string.IsNullOrEmpty(type))
                {
                    type = type.ToLower();
                    query = query.Where(m => m.Type.ToLower().Contains(type));
                }

                if (!string.IsNullOrEmpty(genre))
                {
                    genre = genre.ToLower();
                    query = query.Where(m => m.MediaGenres.Any(g => g.Genre.Id.ToLower().Contains(genre)));
                }

                //TODO: Fix the ordering of media query object so the ranking stays the same as when running the sql function

                // Apply the ordering when needed
                query = query.OrderBy(media => searchResult.First(sr => sr.Id == media.Id).Rank);

                // Continue with other operations
                var result = query
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

                return (result, query.Count());

            }
        }
    }







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

    public (IList<Media> products, int count) Search(int page, int pageSize, string search, string type, string genre)
    {

        search = search.ToLower();


        using (var db = new Context())
        {
            var query = GetMediaWithIncludes(db)
                .Where(m => m.Title.ToLower().Contains(search));

            db.Database.ExecuteSqlInterpolated($"SELECT * FROM search_media(ARRAY['harry','arnold','emma','blue','red','yellow'])");
            if (!string.IsNullOrEmpty(type))
            {
                type = type.ToLower();
                query = query.Where(m => m.Type.ToLower().Contains(type));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                genre = genre.ToLower();
                query = query.Where(m => m.MediaGenres.Any(g => g.Genre.Id.ToLower().Contains(genre)));
            }

            var result = query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return (result, result.Count);
        }
    }
    private Media GetMediaWithIncludes(Context db, string id)
    {
        return db.Media
            .Include(m => m.MediaGenres)
            .Include(c => c.MediaCountries)
            .Include(l => l.MediaLanguages)
            .FirstOrDefault(x => x.Id == id);
    }
    private IQueryable<Media> GetMediaWithIncludes(Context db)
    {
        return db.Media
            .Include(m => m.MediaGenres)
            .Include(c => c.MediaCountries)
            .Include(l => l.MediaLanguages);
    }
    private MediaDTO MapToMediaDTO(Media media, Context db)
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
            Rating = db.Rating.FirstOrDefault(x => x.Id == media.Id)
        };

        return dto;
    }

    public (IList<Team> actors, IList<Team> writersanddirectors, IList<Team> crew) GetActorsForMedia(string m_id)
    {

        using (var db = new Context())
        {
            var team = db.Team
            .Include(m => m.Person)
            .Where(x => x.MediaId == m_id);

            var actors = team.Where(z => z.Role == "actor" || z.Role == "actress" || z.Role == "self").ToList();
            var directors = team.Where(z => z.Role == "director" || z.Role == "writer").ToList();
            var crew = team.Where(z => z.Role != "actor" && z.Role != "actress" && z.Role != "self" && z.Role != "director" && z.Role != "writer").ToList();


            return (actors,directors,crew);
        }
    }
}

