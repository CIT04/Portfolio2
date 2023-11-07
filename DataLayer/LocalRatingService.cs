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
        try
        {
            using var db = new Context();
            var xLocalRating = new LocalRating
            {
                M_id = localRating.M_id,
                U_id = localRating.U_id,
                LocalScore = localRating.LocalScore
            };

            db.Database.ExecuteSqlInterpolated($"SELECT rate({xLocalRating.M_id}, {xLocalRating.U_id}, {xLocalRating.LocalScore})");

            db.SaveChanges();
        }
        catch (Npgsql.PostgresException ex)
        {
            if (ex.SqlState == "P0001") // Custom exception SQL state
            {
                // Handle the custom exception message here
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    //CRUD Update
    public bool UpdateLocalRating(LocalRating localrating)
    {
        using var db = new Context();
        var xLocalRating = db.LocalRating.FirstOrDefault(u => u.M_id == localrating.M_id);

        if (xLocalRating != null) 
        {
            db.Database.ExecuteSqlInterpolated($"select update_LocalRating({localrating.M_id}, {localrating.U_id}, {localrating.LocalScore})");
            db.SaveChanges();
            return true;
        }
        return false;

    }

    //CRUD Delete
    public void DeleteLocalRating(int u_id, string m_id) 
    {
        using var db = new Context();
        db.Database.ExecuteSqlInterpolated($"select delete_localrating_by_mid({u_id}, {m_id})");
        db.SaveChanges();
    }

    

}