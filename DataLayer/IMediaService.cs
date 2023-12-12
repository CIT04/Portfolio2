using DataLayer.Models;
using DataLayer.Objects;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IMediaService
    {
        (IList<Media> products, int count) GetMedias(int page, int pageSize);
        
        MediaDTO? GetMedia( string id);

        IList<Media> GetMediasByTitle(string search);
        (IList<Media> products, int count) GetMediasByGenre(int page, int pageSize, string genre);
        (IList<SearchResult> products, int count) GetMediasBySearch(int page, int pageSize, string  search, string type, int? u_id);
        (IList<Media> products, int count) GetMediasByType(int page, int pageSize, string types);
        (IList<Team> actors, IList<Team> writersanddirectors, IList<Team> crew) GetActorsForMedia(string m_id);
    }
}