using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

//TODO: Try/Catch exception, and write tests for invalid input for ALL methods
public class LocalRatingService : ILocalRatingService
{
    public LocalRating? GetLocalRating(string localrating)
    {
        var db = new Context();
        return db.LocalRating.FirstOrDefault(x => x.M_id == localrating);

    }
    public IEnumerable<LocalRating> GetLocalRatingByUid(int U_id)
    {
        var db = new Context();
        return db.LocalRating.Where(x => x.U_id == U_id).ToList();
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

        //TODO: Fix this part of the code, technically the code works and it wont let the user rate a movie they have already rated, but for some reason we are not catching the exception thrown in SQL
        catch (Npgsql.PostgresException ex)
        {
            if (ex.SqlState == "P0001") // Custom exception SQL state
            {
                // Handle the custom exception message here
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("TESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTESTTESTEST");
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
    public void DeleteLocalRating(LocalRating localrating) 
    {
        using var db = new Context();
        db.Database.ExecuteSqlInterpolated($"select delete_localrating_by_mid({localrating.M_id}, {localrating.U_id})");
        db.SaveChanges();
    }

    

}