using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;

public interface ITeamService

{
    Team? GetTeam(string mediaId);
    Team? GetTeamByMid(string MediaId);
    (IList<Team> products, int count) GetTeams(int page, int pageSize);
}
