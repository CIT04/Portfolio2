using DataLayer.Objects;

namespace DataLayer;

public class RatingService : IRatingService
{
    public Rating? GetRating(string rating)
    {
        var db = new Context();
        return db.Rating.FirstOrDefault(x => x.Ratings == rating);

    }

    public (IList<Rating> products, int count) GetRatings(int page, int pageSize)
    {
        var db = new Context();
        var rating =
            db.Rating
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (rating, db.Rating.Count());
        
    }
}
