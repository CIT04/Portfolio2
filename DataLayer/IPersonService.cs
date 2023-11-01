using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;

public interface IPersonService

{
    Person? GetPerson(string id);
    Person? GetPersonByPId(string Id);
    (IList<Person> products, int count) GetPersons(int page, int pageSize);
}
