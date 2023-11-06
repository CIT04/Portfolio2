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

   
    protected object Paging<T>(IEnumerable<T> items, int total,SearchParams searchParams, string endpointName)
    {


        var numPages = (int)Math.Ceiling(total / (double)searchParams.pageSize);
        var next = searchParams.page < numPages - 1
            ? GetUrl(endpointName, new { page = searchParams.page + 1, searchParams.pageSize, searchParams.Genre, searchParams.Type, searchParams.Search })
        : null;
        var prev = searchParams.page > 0
            ? GetUrl(endpointName, new { page = searchParams.page - 1, searchParams.pageSize, searchParams.Genre, searchParams.Type, searchParams.Search })
        : null;

        var cur = GetUrl(endpointName, new { searchParams.page, searchParams.pageSize, searchParams.Genre, searchParams.Type, searchParams.Search });



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

    protected string GetUrl(string name, object values)
    {
        var url = _linkGenerator.GetUriByName(HttpContext, name, values);
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

