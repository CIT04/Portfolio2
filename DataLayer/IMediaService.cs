using DataLayer.Models;
using DataLayer.Objects;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IMediaService
    {
        (IList<Media> products, int count) GetMedias(int page, int pageSize);
        
        Media? GetMedia(string id);

        IList<Media> GetMediasByTitle(string search);
        (IList<Media> products, int count) GetMediasByGenre(int page, int pageSize, string search);
    }
}