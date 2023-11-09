using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;


namespace DataLayer;


public class Context : DbContext
{
    
    public DbSet<Objects.Person> Person { get; set; }
    public DbSet<Objects.Media> Media { get; set; }
    public DbSet<Objects.Team> Team { get; set; }
    public DbSet<Objects.User> User { get; set; }
    public DbSet<Objects.Country> Country { get; set; }
    public DbSet<Objects.Language> Language { get; set; }
    public DbSet<Objects.Genre> Genre { get; set; }
    public DbSet<Objects.MediaGenre> MediaGenre { get; set; }
    public DbSet<Objects.Rating> Rating { get; set; }
    public DbSet<Objects.Bookmark> Bookmarks { get; set; }
    public DbSet<Objects.History> History { get; set; }
    public DbSet<Objects.LocalRating> LocalRating { get; set; }
    public DbSet<Objects.SearchHistory> SearchHistory { get; set; }
    public DbSet<Objects.SeasonEpisode> SeasonEpisode{ get; set; }
    //public object Bookmarks { get; internal set; }
    public DbSet<Objects.SearchResult> SearchResult { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=xbSNeklDajCG");
        optionsBuilder.EnableSensitiveDataLogging();

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //media
        modelBuilder.Entity<Objects.Media>().ToTable("media");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Plot).HasColumnName("plot");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Released).HasColumnName("released");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Year).HasColumnName("year");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Poster).HasColumnName("poster");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Runtime).HasColumnName("runtime");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.IsAdult).HasColumnName("isadult");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.EndYear).HasColumnName("endyear");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Rated).HasColumnName("rated");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Awards).HasColumnName("awards");
        modelBuilder.Entity<Objects.Media>()
            .Property(x => x.Type).HasColumnName("type");
        


        //genre
        modelBuilder.Entity<Objects.Genre>().ToTable("genre");
        modelBuilder.Entity<Objects.Genre>()
            .Property(x => x.Id).HasColumnName("genre");

        modelBuilder.Entity<Objects.MediaGenre>().ToTable("mediagenre");
        modelBuilder.Entity<Objects.MediaGenre>().HasKey(x => new { x.MediaId, x.GenreId });
        modelBuilder.Entity<Objects.MediaGenre>()
            .Property(x => x.MediaId).HasColumnName("m_id");
        modelBuilder.Entity<Objects.MediaGenre>()
            .Property(x => x.GenreId).HasColumnName("genre");

      
        //country
        modelBuilder.Entity<Objects.Country>().ToTable("country");
        modelBuilder.Entity<Objects.Country>()
        .ToTable("country")
        .HasKey(x => new { x.Id });
        modelBuilder.Entity<Objects.Country>()
            .Property(x => x.Id).HasColumnName("country");

        modelBuilder.Entity<Objects.MediaCountry>().ToTable("mediacountry");
        modelBuilder.Entity<Objects.MediaCountry>().HasKey(x => new { x.MediaId, x.CountryId });
        modelBuilder.Entity<Objects.MediaCountry>()
            .Property(x => x.MediaId).HasColumnName("m_id");
        modelBuilder.Entity<Objects.MediaCountry>()
           .Property(x => x.CountryId).HasColumnName("country");


        //language 
        modelBuilder.Entity<Objects.Language>().ToTable("language");
        modelBuilder.Entity<Objects.Language>()
        .ToTable("language")
        .HasKey(x => new { x.Id });
        modelBuilder.Entity<Objects.Language>()
            .Property(x => x.Id).HasColumnName("language");

        modelBuilder.Entity<Objects.MediaLanguage>().ToTable("medialanguage");
        modelBuilder.Entity<Objects.MediaLanguage>().HasKey(x => new { x.MediaId, x.LanguageId });
        modelBuilder.Entity<Objects.MediaLanguage>()
            .Property(x => x.MediaId).HasColumnName("m_id");
        modelBuilder.Entity<Objects.MediaLanguage>()
           .Property(x => x.LanguageId).HasColumnName("language");


        //season episode 
        modelBuilder.Entity<Objects.SeasonEpisode>().ToTable("seasonepisode");
        modelBuilder.Entity<Objects.SeasonEpisode>()
        .HasKey(x => x.Id);

        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.Id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.Episode).HasColumnName("episode");
        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.TotalSeasons).HasColumnName("totalseasons");


        //Person 
        modelBuilder.Entity<Objects.Person>().ToTable("person");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.Id).HasColumnName("p_id");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.Name).HasColumnName("primaryname");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.DeathYear).HasColumnName("deathyear");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.PrimaryProfession).HasColumnName("primaryprofession");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.KnownForTitles).HasColumnName("knownfortitles");
        modelBuilder.Entity<Objects.Person>()
            .Property(x => x.NameRating).HasColumnName("name_rating");


        //Team
        modelBuilder.Entity<Objects.Team>().ToTable("team");
        modelBuilder.Entity<Objects.Team>().HasKey(x => new { x.MediaId, x.PersonId });
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.MediaId).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.PersonId).HasColumnName("p_id");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.Role).HasColumnName("role");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.Characters).HasColumnName("characters");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.Job).HasColumnName("job");


        //user
        modelBuilder.Entity<Objects.User>().ToTable("user");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Username).HasColumnName("username");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.FirstName).HasColumnName("firstname");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.LastName).HasColumnName("lastname");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Dob).HasColumnName("dob");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Salt).HasColumnName("salt");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Role).HasColumnName("role");

        //Rating
        modelBuilder.Entity<Objects.Rating>().ToTable("rating2");
        modelBuilder.Entity<Objects.Rating>().HasKey(x => x.Id);
       
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.Id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.ImdbRatings).HasColumnName("imdbratings");
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.Ratings).HasColumnName("ratings");
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.ImdbVotes).HasColumnName("imdbvotes");
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.NumVotes).HasColumnName("numvotes");


        //Bookmarks
        modelBuilder.Entity<Objects.Bookmark>().ToTable("bookmarks");
        modelBuilder.Entity<Objects.Bookmark>()
        .HasKey(x => new { x.M_id, x.U_id });
        modelBuilder.Entity<Objects.Bookmark>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Bookmark>()
            .Property(x => x.U_id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.Bookmark>()
            .Property(x => x.Time).HasColumnName("time");
        modelBuilder.Entity<Objects.Bookmark>()
            .Property(x => x.Annotation).HasColumnName("annotation");
        modelBuilder.Entity<Objects.Bookmark>();


        //History
        modelBuilder.Entity<Objects.History>().ToTable("history");
        modelBuilder.Entity<Objects.History>()
        .HasKey(x => new { x.M_id, x.U_id });
        modelBuilder.Entity<Objects.History>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.History>()
            .Property(x => x.U_id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.History>()
            .Property(x => x.Time).HasColumnName("time");
        modelBuilder.Entity<Objects.History>();

        //SearchHistory
        modelBuilder.Entity<Objects.SearchHistory>().ToTable("search_history");
        modelBuilder.Entity<Objects.SearchHistory>()
        .HasKey(x => new { x.U_id });
        modelBuilder.Entity<Objects.SearchHistory>()
            .Property(x => x.Search_string).HasColumnName("search_string");
        modelBuilder.Entity<Objects.SearchHistory>()
            .Property(x => x.U_id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.SearchHistory>()
            .Property(x => x.Time).HasColumnName("time");


        //LocalRating
        modelBuilder.Entity<Objects.LocalRating>().ToTable("localrating");
        modelBuilder.Entity<Objects.LocalRating>()
        .HasKey(x => new { x.M_id, x.U_id });
        modelBuilder.Entity<Objects.LocalRating>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.LocalRating>()
            .Property(x => x.U_id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.LocalRating>()
            .Property(x => x.LocalScore).HasColumnName("localscore");
        modelBuilder.Entity<Objects.LocalRating>();

        modelBuilder.Entity<Objects.SearchResult>().HasNoKey();
        modelBuilder.Entity<Objects.SearchResult>().Property(x => x.Id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.SearchResult>().Property(x => x.Rank).HasColumnName("rank");
        modelBuilder.Entity<Objects.SearchResult>().Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<Objects.SearchResult>().Property(x => x.Type).HasColumnName("type");
        modelBuilder.Entity<Objects.SearchResult>().Property(x => x.Genre).HasColumnName("genre");

    }
}

