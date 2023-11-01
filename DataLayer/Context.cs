﻿using DataLayer.Objects;
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

    public DbSet<Objects.SeasonEpisode> SeasonEpisode{ get; set; }
    //public object Bookmarks { get; internal set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=xbSNeklDajCG");
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
        modelBuilder.Entity<Objects.MediaCountry>().ToTable("mediacountry");
        modelBuilder.Entity<Objects.MediaCountry>().HasKey(x => new { x.MediaId, x.CountryId });
        modelBuilder.Entity<Objects.MediaCountry>()
            .Property(x => x.MediaId).HasColumnName("m_id");
        modelBuilder.Entity<Objects.MediaCountry>()
           .Property(x => x.CountryId).HasColumnName("country");
 
        modelBuilder.Entity<Objects.Country>().ToTable("country");
        modelBuilder.Entity<Objects.Country>()
        .ToTable("country")
        .HasKey(x => new { x.country });
        modelBuilder.Entity<Objects.Country>()
            .Property(x => x.country).HasColumnName("country");

        //language 
        modelBuilder.Entity<Objects.Language>().ToTable("language");
        modelBuilder.Entity<Objects.Language>()
        .ToTable("language")
        .HasKey(x => new { x.language });
        modelBuilder.Entity<Objects.Language>()
            .Property(x => x.language).HasColumnName("language");


        //season episode 
        modelBuilder.Entity<Objects.SeasonEpisode>().ToTable("seasonepisode");
        modelBuilder.Entity<Objects.SeasonEpisode>()
    .HasKey(x => x.M_id);

        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.Episode).HasColumnName("episode");
        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.SeasonNumber).HasColumnName("seasonnumber");
        modelBuilder.Entity<Objects.SeasonEpisode>()
            .Property(x => x.TotalSeasons).HasColumnName("totalseasons");


        //Person 
        modelBuilder.Entity<Objects.Person>().ToTable("person");
        modelBuilder.Entity<Objects.Person>()
           .Property(x => x.Id).HasColumnName("p_id");
        modelBuilder.Entity<Objects.Person>()
          .Property(x => x.Name).HasColumnName("primaryname");

        //Team
        modelBuilder.Entity<Objects.Team>().ToTable("team");
        modelBuilder.Entity<Objects.Team>().HasKey(x => new { x.M_id, x.P_id });
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.P_id).HasColumnName("p_id");

        //user
        modelBuilder.Entity<Objects.User>().ToTable("user");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Username).HasColumnName("username");

        //Rating
        modelBuilder.Entity<Objects.Rating>().ToTable("rating");
        modelBuilder.Entity<Objects.Rating>()
    .ToTable("rating")
    .HasKey(x => new { x.Ratings });
        modelBuilder.Entity<Objects.Rating>()
            .Property(x => x.M_id).HasColumnName("m_id");
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


    }
}

