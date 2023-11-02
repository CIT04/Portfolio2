using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;


public interface IHistoryService

{
    History? GetHistory(string Id);

    (IList<History> products, int count) GetHistories(int page, int pageSize);
}
