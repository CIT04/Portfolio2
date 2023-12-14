using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("{id:int}", Name = nameof(GetUser))]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUser(int id)
    {
        try
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _dataService.GetUserByUsername(userName);
            var userToGet = _dataService.GetUser(id);
            if (userToGet == null)
            {
                return NotFound();
            }

            return Ok(CreateUserModel(userToGet));
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpGet("{username}", Name = nameof(GetUserByUsername))]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUserByUsername(string username)
    {
        try { 
        var userName = HttpContext.User.Identity.Name;
        var user = _dataService.GetUserByUsername(userName);
        var userToGet = _dataService.GetUserByUsername(username);

        if (userToGet == null) {
            return NotFound();
        }

        return Ok(CreateUserModel(userToGet));
    }
        catch { return Unauthorized();
}
    }

    [HttpGet(Name = nameof(GetUsers))]
    [Authorize(Roles = "Admin")]
    public IActionResult GetUsers([FromQuery] SearchParams searchParams)
    {
        try
        {
        var userName = HttpContext.User.Identity.Name;
        var user = _dataService.GetUserByUsername(userName);
        UpdateSearchParamsFromQuery(searchParams);
        (var users, var total) = _dataService.GetUsers(searchParams.page, searchParams.pageSize);

        var items = users.Select(CreateUserModel);

        var result = Paging(items, total, searchParams, nameof(GetUsers));

        return Ok(result);
        }
        catch { return Unauthorized(); }

    }

    [HttpPost]
    [Authorize(Roles = "Admin")]

    public IActionResult CreateUser(CreateUserModel userToCreate)
    {
        try
        {
            var userName = HttpContext.User.Identity.Name;
            var user = _dataService.GetUserByUsername(userName);

            var xUser = new User
        {
            Username = userToCreate.Username,
            Password = userToCreate.Password,
            FirstName = userToCreate.FirstName,
            LastName = userToCreate.LastName,
            Email = userToCreate.Email,
            Dob = userToCreate.Dob,
            Role = userToCreate.Role
        };

        _dataService.CreateUser(xUser);

        return Created($"api/user/{xUser.Id}", xUser);
        }
        catch { return Unauthorized(); }
    }
   
    //TODO: Authorise the user to update themselves
    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateUser(User userToUdate)
    {
        try {
            var userName = HttpContext.User.Identity.Name;
            var user = _dataService.GetUserByUsername(userName);

            var result = _dataService.UpdateUser(userToUdate);
        if (result)
        { return Ok(userToUdate); }
        return NotFound();
          }
        catch { return Unauthorized(); }
    }

    //TODO: Authorize users to delete their own accounts
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(int id)
    {
        var userName = HttpContext.User.Identity.Name;
        var user = _dataService.GetUserByUsername(userName);
        var userForDelete = _dataService.GetUser(id);
        if (userForDelete == null)
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

    [HttpPost("signup")]
    public IActionResult SignUp(User model)
    {
        if (_dataService.GetUser(model.Id) != null) 
        { 
            return BadRequest("Username already exists.");
        }
        if (model.Username == null)
        {
            return BadRequest("Username cannot be empty.");
        }
        if (model.Username.Length < 4 || model.Username.Length > 16 )
        {
            return BadRequest("Username must be between 4 and 16 characters.");
        }

        if (_dataService.IsEmailAlreadyRegistered(model.Email))
        {
            return BadRequest("Email already registered. Want to login?");
        }
        if (!_dataService.IsValidEmail(model.Email))
        {
            return BadRequest("Invalid email format.");
        }

        if (model.Email == null)
        {
            return BadRequest("Email cannot be empty.");
        }

        if (model.Password.Length < 8 || model.Password.Length > 16)
        {
            return BadRequest("Password must be between 8 and 16 characters.");
        }

        if (!model.Password.Any(char.IsUpper))
        {
            return BadRequest("Password must contain at least one uppercase character.");
        }
        if (string.IsNullOrEmpty(model.Password))
        {
            return BadRequest("Password cannot be empty.");
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
            expires: DateTime.Now.AddDays(4),
            signingCredentials: creds

            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        
        return Ok(new {user.Id, user.Username, token = jwt});
    }

}
