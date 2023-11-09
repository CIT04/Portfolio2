using DataLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;
//TODO: Try/Catch exception, and write tests for invalid input for ALL methods
public class LanguageService :  ILanguageService
{

    public Language? GetLanguage(string language)
    {
        var db = new Context();
        return db.Language.FirstOrDefault(x => x.Id == language);

    }

    public (IList<Language> products, int count) GetLanguages(int page, int pageSize)
    {
        var db = new Context();
        var language =
            db.Language
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (language, db.Language.Count());
    }
}

