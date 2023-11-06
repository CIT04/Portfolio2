using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class LocalRatingService : ILocalRatingService
{
    public LocalRating? GetLocalRating(string localrating)
    {
        var db = new Context();
        return db.LocalRating.FirstOrDefault(x => x.M_id == localrating);

    }

    public (IList<LocalRating> products, int count) GetLocalRatings(int page, int pageSize)
    {
        var db = new Context();
        var localrating =
            db.LocalRating
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (localrating, db.LocalRating.Count());
    }

    public void CreateLocalRating(LocalRating localRating)
    {
        using var db = new Context();
        var xLocalRating = new LocalRating
        {
            M_id = localRating.M_id,
            U_id = localRating.U_id,
            LocalScore = localRating.LocalScore
        };



        db.Database.ExecuteSqlInterpolated($"select insert_localrating({xLocalRating.M_id}, {xLocalRating.U_id}, {xLocalRating.LocalScore})");

        db.SaveChanges();
    }
}