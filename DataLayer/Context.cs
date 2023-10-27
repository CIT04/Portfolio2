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
    public DbSet<Objects.Country> Country{ get; set; }
    public DbSet<Objects.Language> Language { get; set; }

    public DbSet<Objects.SeasonEpisode> SeasonEpisode{ get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder
            .LogTo(Console.Out.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=xbSNeklDajCG");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Objects.Person>().ToTable("person");
        modelBuilder.Entity<Objects.Person>()
           .Property(x => x.Id).HasColumnName("p_id");
        modelBuilder.Entity<Objects.Person>()
          .Property(x => x.Name).HasColumnName("primaryname");

        modelBuilder.Entity<Objects.Team>().ToTable("team");
        modelBuilder.Entity<Objects.Team>().HasKey(x => new { x.M_id, x.P_id });
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.M_id).HasColumnName("m_id");
        modelBuilder.Entity<Objects.Team>()
            .Property(x => x.P_id).HasColumnName("p_id");


        modelBuilder.Entity<Objects.User>().ToTable("user");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Id).HasColumnName("u_id");
        modelBuilder.Entity<Objects.User>()
            .Property(x => x.Username).HasColumnName("username");


        modelBuilder.Entity<Objects.Country>().ToTable("country");
        modelBuilder.Entity<Objects.Country>()
    .ToTable("country")
    .HasKey(x => new {  x.country });
        modelBuilder.Entity<Objects.Country>()
            .Property(x => x.country).HasColumnName("country");


        modelBuilder.Entity<Objects.Language>().ToTable("language");
        modelBuilder.Entity<Objects.Language>()
    .ToTable("language")
    .HasKey(x => new { x.language });
        modelBuilder.Entity<Objects.Language>()
            .Property(x => x.language).HasColumnName("language");


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



    }
}

