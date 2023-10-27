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


        (IList<Country> products, int count) GetCountries(int page, int pageSize);
        Country? GetCountry(string country);
<<<<<<< Updated upstream:DataLayer/IMediaService.cs
        
=======

        (IList<Language> products, int count) GetLanguages(int page, int pageSize);
        Language? GetLanguage (string language);

        IList<ActorsForMediaDTO> GetActorsForMedia(int page, int pageSize, string m_id);
>>>>>>> Stashed changes:DataLayer/IDataService.cs
    }
}