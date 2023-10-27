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
    private readonly IMediaService _dataService;
    public UserController(IMediaService dataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;


    }

    [HttpGet("{id}", Name = nameof(GetUser))]
    public IActionResult GetUser(string id)
    {
        var user = _dataService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(CreateUserModel(user));
    }
    [HttpGet(Name = nameof(GetUsers))]
    public IActionResult GetUsers(int page = 0, int pageSize = 10)
    {
        (var users, var total) = _dataService.GetUsers(page, pageSize);

        var items = users.Select(CreateUserModel);

        var result = Paging(items, total, page, pageSize, nameof(GetUsers));

        return Ok(result);
    }
    private UserModel CreateUserModel(User user)
    {
        return new UserModel
        {
            Url = GetUrl(nameof(GetUsers), new { user.Id }),
            Username = user.Username,
            Id = user.Id

        };

    }

}
