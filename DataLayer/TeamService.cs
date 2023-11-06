using DataLayer.Objects;

namespace DataLayer;

public class TeamService : ITeamService
{
    public Team? GetTeam(string team)
    {
        var db = new Context();
        return db.Team.FirstOrDefault(x => x.MediaId == team);

    }
    public Team? GetTeamByMid(string mediaid) 
    {
        var db = new Context();
        return db.Team.FirstOrDefault(x => x.MediaId == mediaid);
    }
    
    
    public (IList<Team> products, int count) GetTeams(int page, int pageSize)
    {
        var db = new Context();
        var team =
            db.Team
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (team, db.Team.Count());
        
    }
}
