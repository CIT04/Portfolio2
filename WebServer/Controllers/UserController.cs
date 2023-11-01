﻿using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using WebServer.Models;

namespace WebServer.Controllers;


[Route("api/user")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _dataService;
    public UserController(IUserService dataService, LinkGenerator linkGenerator)
      : base(linkGenerator)
    {
        _dataService = dataService;


    }

    [HttpGet("{id}", Name = nameof(GetUser))]
    public IActionResult GetUser(int id)
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

    [HttpPost]
    public IActionResult CreateUser(CreateUserModel user)
    {
        var xUser = new User
        {
            Username = user.Username,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Dob = user.Dob
        };

        _dataService.CreateUser(xUser);

        return Created($"api/user/{xUser.Id}", xUser);
    }

    //[HttpDelete("{id}")]
    //public IActionResult DeleteUser(int id)
    //{
    //    _dataService.DeleteUser(id);

      
    //    return Delete(delete);
    //}

    private UserModel CreateUserModel(User user)
    {
        return new UserModel
        {
            Url = GetUrl(nameof(GetUsers), new { user.Id }),
            Username = user.Username,
            Id = user.Id,
            Password = user.Password,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Dob = user.Dob

        };

    }
    

}
