using DataLayer.Models;
using DataLayer.Objects;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IDataService
    {
        (IList<Media> products, int count) GetMedias(int page, int pageSize);
        
        Media? GetMedia(string id);

        User? GetUser(string id);

        IList<Media> GetMediasByTitle(string search);

        (IList<User>  products, int count) GetUsers(int page, int pageSize);
    }
}