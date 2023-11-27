using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Objects;
namespace DataLayer

//TODO: Try/Catch exception, and write tests for invalid input for ALL methods
{
    public class ActorService: IActorService
    {
        public (IList<Person> products, int count) GetActors(int page, int pageSize)
        {
            var db = new Context();
            var actors =
                db.Person
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return (actors, db.Media.Count());
        }

        public IList<ActorsForMediaDTO> GetActorsForMedia(int page, int pageSize, string MediaId)
        {
            var db = new Context();

            // Use LINQ to query the database
            var actorsForMedia = (from team in db.Team
                                    where team.MediaId == MediaId
                                    join person in db.Person on team.PersonId equals person.Id
                                    select new ActorsForMediaDTO
                                    {
                                        Id = person.Id,
                                        Name = person.Name
                                    })
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return actorsForMedia;

        }
        public IList<ActorsForMediaDTO> GetActorsObject(int page, int pageSize, string p_id)
        {
            var db = new Context();

            // Use LINQ to query the database
            var actorsForMedia = (from person in db.Person
                                  where person.Id == p_id
                                  join team in db.Team on person.Id equals team.PersonId
                                  select new ActorsForMediaDTO
                                  {
                                      Id = person.Id,
                                      Name = person.Name,
                                      Birthyear = person.BirthYear,
                                      KnownForTitles = person.KnownForTitles
                                      
                                  })
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return actorsForMedia;

        }

    }
}
