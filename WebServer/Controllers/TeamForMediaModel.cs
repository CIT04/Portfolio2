using DataLayer.Objects;
namespace WebServer.Controllers
{
    public class TeamForMediaModel
    {
        public IList<Team> Actor {  get; set; }
        public IList<Team> WritersAndDirectors {  get; set; }
        public IList<Team> Crew {  get; set; }


    }
}