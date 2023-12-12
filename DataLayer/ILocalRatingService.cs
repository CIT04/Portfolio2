using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;


public interface ILocalRatingService

{
    LocalRating? GetLocalRating(string Id);
    IEnumerable<LocalRating> GetLocalRatingByUid(int U_id);
    double GetLocalRating(int u_id, string m_id);
    (IList<LocalRating> products, int count) GetLocalRatings(int page, int pageSize);

    void CreateLocalRating(LocalRating localRating);
    void DeleteLocalRating(LocalRating localRating);
    bool UpdateLocalRating(LocalRating localrating);
}
