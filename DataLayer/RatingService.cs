using DataLayer.Objects;

namespace DataLayer;

//TODO: Try/Catch exception, and write tests for invalid input for ALL methods

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
        return db.Rating.FirstOrDefault(x => x.Id == m_id);
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
