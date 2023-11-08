using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebServer.Models;
using WebServiceToken.Services;

namespace WebServer.Controllers;


[Route("api/user")]
[ApiController]
public class UserController : BaseController
{
    private readonly IUserService _dataService;
    private readonly Hashing _hashing;
    private readonly IConfiguration _configuration;

    public UserController(IUserService dataService, LinkGenerator linkGenerator, Hashing hashing, IConfiguration configuration)
      : base(linkGenerator)
    {
        _dataService = dataService;
        _hashing = hashing;
        _configuration = configuration;
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

    [HttpGet("{id}", Name = nameof(GetUser))]
    public IActionResult GetUserByUsername(string username)
    {
        var user = _dataService.GetUserByUsername(username);
        
        if(user == null){
            return NotFound();
        }

        return Ok(CreateUserModel(user));
    }

    [HttpGet(Name = nameof(GetUsers))]
    public IActionResult GetUsers([FromQuery] SearchParams searchParams)
    {
        UpdateSearchParamsFromQuery(searchParams);
        (var users, var total) = _dataService.GetUsers(searchParams.page, searchParams.pageSize);

        var items = users.Select(CreateUserModel);

        var result = Paging(items, total,searchParams, nameof(GetUsers));

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
            Dob = user.Dob,
            Role = user.Role
        };

        _dataService.CreateUser(xUser);

        return Created($"api/user/{xUser.Id}", xUser);
    }
    //[HttpPost]
    //public IActionResult CreateUser(string username, string password, string firstname, string lastname,string email, string dob, string salt)
    //{
    //    var xUser = new User
    //    {
    //        Username = username,
    //        Password = password,
    //        FirstName = firstname,
    //        LastName = lastname,
    //        Email = email,
    //        Dob = dob,
    //        Salt = salt
    //    };

    //    _dataService.CreateUser(xUser);

    //    return Created($"api/user/{xUser.Id}", xUser);
    //}

    [HttpPut("update")]
    public IActionResult UpdateUser(User user)
    {

        var result=_dataService.UpdateUser(user);
        if (result) 
        { return Ok(user); }
        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _dataService.GetUser(id);
        if (user == null)
        {
            return NotFound();
        }
        _dataService.DeleteUser(id);
        return Ok("User Deleted");
    }
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
            Dob = user.Dob,
            Salt = user.Salt,
            Role = user.Role

        };

    }

    [HttpPost("signin")]
    public IActionResult SignIn(User model)
    {
        if (_dataService.GetUser(model.Id) != null) 
        { 
            return BadRequest();
        }

        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest();
        }

        (var hashedPwd, var salt) = _hashing.Hash(model.Password);
        model.Password = hashedPwd;
        model.Salt = salt;
       
        _dataService.CreateUser(model);

        return Ok();
    }
    [HttpPost("login")]
    public IActionResult Login (UserLoginModel model)
    {
        var user = _dataService.GetUserByUsername(model.UserName);
        if (user == null) 
        {
            return BadRequest();
                
        }

        if(!_hashing.Verify(model.Password, user.Password, user.Salt)) 
        {
            return BadRequest();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)

        };

        var secret = _configuration.GetSection("Auth:Secret").Value;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddSeconds(45),
            signingCredentials: creds

            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        
        return Ok(new {user.Username, token = jwt});
    }

}
