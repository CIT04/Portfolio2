using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



public interface IRatingService

{
    Rating? GetRating(string id);

    (IList<Rating> products, int count) GetRatings(int page, int pageSize);
}
