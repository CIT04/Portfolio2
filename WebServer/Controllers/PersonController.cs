using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/person")]
[ApiController]
public class PersonController : BaseController
{
    private readonly IPersonService _dataService;

    public PersonController(IPersonService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetPersons))]

    public IActionResult GetPersons([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var persons, var total) = _dataService.GetPersons(searchParams.page, searchParams.pageSize);

        var items = persons.Select(CreatePersonModel);

        var result = Paging(items, total, searchParams, nameof(GetPersons));

        return Ok(result);
    }

    [HttpGet("{Id}", Name = nameof(GetPerson))]
    public IActionResult GetPerson(string Id)
    {
        var person1 = _dataService.GetPersonByPId(Id);

        if (person1 == null)
        {
            return NotFound();
        }

        return Ok(CreatePersonModel(person1));
    }

    public PersonModel CreatePersonModel(Person person)
    {
        return new PersonModel
        {
            Url = GetUrl(nameof(GetPerson), new { person.Id }),

            //Person = person.Persons,
            P_id = person.Id,
            PrimaryName = person.Name,
            BirthYear = person.BirthYear,
            DeathYear = person.DeathYear,
            PrimaryProfession = person.PrimaryProfession,
            KnownForTitles = person.KnownForTitles,
            NameRating = person.NameRating,
        };
    }
}

