using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;


public interface ILocalRatingService

{
    LocalRating? GetLocalRating(string Id);

    (IList<LocalRating> products, int count) GetLocalRatings(int page, int pageSize);
}
