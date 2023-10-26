using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebServer.Models;

namespace WebServer.Controllers;

[Route("api/User")]
[ApiController]
public class UserController : BaseController
{
    private readonly IDataService _dataService;
    public UserController(IDataService dataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;

    }

}

