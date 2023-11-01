using DataLayer.Objects;

namespace DataLayer;

public class RatingService : IRatingService
{
    public Rating? GetRating(string rating)
    {
        var db = new Context();
        return db.Rating.FirstOrDefault(x => x.Ratings == rating);

    }
    public Rating? GetRatingByMId(string m_id) 
    {
        var db = new Context();
        return db.Rating.FirstOrDefault(x => x.M_id == m_id);
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
