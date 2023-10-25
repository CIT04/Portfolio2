using DataLayer.Models;
using DataLayer.Objects;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        (IList<Media> products, int count) GetMedias(int page, int pageSize);
    }
}