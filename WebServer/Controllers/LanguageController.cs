﻿using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/language")]
[ApiController]
public class LanguageController : BaseController
{
    private readonly ILanguageService _dataService;

    public LanguageController(ILanguageService dataService, LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;

    }

    [HttpGet(Name = nameof(GetLanguages))]

    public IActionResult GetLanguages(int page = 0, int pageSize = 10)
    {
        (var languages, var total) = _dataService.GetLanguages(page, pageSize);

        var items = languages.Select(CreateLanguageModel);

        //var result = Paging(items, total, page, pageSize, nameof(GetLanguages));
        return BadRequest();
        //return Ok(result);
    }

    [HttpGet("{language}", Name = nameof(GetLanguage))]
    public IActionResult GetLanguage(string language)
    {
        var language1 = _dataService.GetLanguage(language);
        if (language1 == null)
        {
            return NotFound();
        }

        return Ok(CreateLanguageModel(language1));

    }

    public LanguageModel CreateLanguageModel(Language language)
    {
        return new LanguageModel
        {
            Url = GetUrl(nameof(GetLanguages), new { language.Id }),

            Language = (string)language.Id

        };




    }
}

