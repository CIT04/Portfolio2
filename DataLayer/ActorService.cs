﻿using DataLayer.Models;
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

        public IList<ActorsForMediaDTO> GetActorsForMedia( string m_id)
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
                                  .ToList();

            return actorsForMedia;

        }
    }
}
