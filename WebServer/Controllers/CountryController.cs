using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/country")]
[ApiController]
public class CountryController : BaseController
{
    private readonly IMediaService _dataService;

    public CountryController(IMediaService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetCountries))]

    public IActionResult GetCountries(int page = 0, int pageSize = 10)
    {
        (var countries, var total) = _dataService.GetCountries(page, pageSize);

        var items = countries.Select(CreateCountryModel);

        var result = Paging(items, total, page, pageSize, nameof(GetCountries));

        return Ok(result);
    }

    [HttpGet("{country}", Name = nameof(GetCountry))]
    public IActionResult GetCountry(string country)
    {
        var countryq = _dataService.GetCountry(country);
        if (countryq == null)
        {
            return NotFound();
        }

        return Ok(CreateCountryModel(countryq));

    }
    private CountryModel CreateCountryModel(Country country)
    {
        return new CountryModel
        {
            Url = GetUrl(nameof(GetCountries), new { country.country }),

            Country = country.country

        };




    }
}

