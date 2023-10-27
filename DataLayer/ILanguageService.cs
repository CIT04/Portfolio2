using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



public interface ILanguageService

{
    Language? GetLanguage(string id);

    (IList<Language> products, int count) GetLanguages(int page, int pageSize);
}
