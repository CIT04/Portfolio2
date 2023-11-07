using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace WebServer.Controllers;

public class BaseController : ControllerBase
{
    private readonly LinkGenerator _linkGenerator;

    public BaseController(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }


    protected object Paging<T>(IEnumerable<T> items, int total, SearchParams searchParams, string endpointName)
    {
        string[] words = !string.IsNullOrWhiteSpace(searchParams.search)
            ? searchParams.search.ToLower().Split(' ')
            : new string[0]; // Empty array if searchParams.search is null or empty

        var numPages = (int)Math.Ceiling(total / (double)searchParams.pageSize);
        var next = searchParams.page < numPages - 1
            ? GetUrl(endpointName, new { page = searchParams.page + 1, searchParams.pageSize, searchParams.Genre, searchParams.Type }, words)
            : null;
        var prev = searchParams.page > 0
            ? GetUrl(endpointName, new { page = searchParams.page - 1, searchParams.pageSize, searchParams.Genre, searchParams.Type }, words)
            : null;

        var cur = GetUrl(endpointName, new { searchParams.page, searchParams.pageSize, searchParams.Genre, searchParams.Type }, words);

        return new
        {
            Total = total,
            NumberOfPages = numPages,
            Next = next,
            Prev = prev,
            Current = cur,
            Items = items
        };
    }



    //TODO: Create generic geturl
    protected string GetUrl(string name, object values)
    {
        var url = _linkGenerator.GetUriByName(HttpContext, name, values);
        if (url != null)
        {
            url = url.Replace("%20", ""); 
        }
        return url ?? "Not specified";
    }
    protected string GetUrl(string name, object values, string[] words)
    {
        // Join the words together into a single string
        string searchWords = string.Join(" ", words);

        // Add the search parameter properly
        string searchParam = !string.IsNullOrWhiteSpace(searchWords) ? $"&search={searchWords.Replace(" ", "+")}" : "";

        // Add the search parameter to the URL
        var url = _linkGenerator.GetUriByName(HttpContext, name, values) + searchParam;

        if (url != null)
        {
            url = url.Replace("%20", "");
        }

        return url ?? "Not specified";
    }


    public void UpdateSearchParamsFromQuery(SearchParams searchParams)
    {
        if (Request.Query.ContainsKey("page") && int.TryParse(Request.Query["page"], out int page))
        {
            searchParams.page = page;
        }

        if (Request.Query.ContainsKey("pageSize") && int.TryParse(Request.Query["pageSize"], out int pageSize))
        {
            searchParams.pageSize = pageSize;
        }
    }


}

