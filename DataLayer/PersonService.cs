using DataLayer.Objects;

namespace DataLayer;

public class PersonService : IPersonService
{
    public Person? GetPerson(string person)
    {
        var db = new Context();
        return db.Person.FirstOrDefault(x => x.Id == person);

    }
    public Person? GetPersonByPId(string p_id) 
    {
        var db = new Context();
        return db.Person.FirstOrDefault(x => x.Id == p_id);
    }
    
    
    public (IList<Person> products, int count) GetPersons(int page, int pageSize)
    {
        var db = new Context();
        var person =
            db.Person
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
        return (person, db.Person.Count());
        
    }
}
