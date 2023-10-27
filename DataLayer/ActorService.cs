using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Objects;
namespace DataLayer
{
    public class ActorService: IActorService
    {
        public IList<ActorsForMediaDTO> GetActorsForMedia(int page, int pageSize, string m_id)
        {
            var db = new Context();

            // Use LINQ to query the database
            var actorsForMedia = (from team in db.Team
                                  where team.M_id == m_id
                                  join person in db.Person on team.P_id equals person.Id
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
    }
}
