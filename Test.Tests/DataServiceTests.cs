using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebServer.Controllers;
using WebServiceToken.Services;


namespace Assignment4.Tests;

public class WEBSERVERTESTS
{


    public class UserControllerTests
    {
    //TODO: FIX THIS TEST

        // TEST SIGN IN WITH VALID USER CREATES USER AND RETURNS OK RESULT
        [Fact]
        public void GetUser_Controller()
        {
            // Arrange
            var Service = new UserService();
            var hashing = new Hashing();
            var controller = new UserController(Service, null, hashing, null);
            var userModel = new WebServer.Models.CreateUserModel()

            {
                Id = 1000,
                Username = "Ha22s333322h3322ing2233222222229",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Dob = "2020-20-20",
                Email = "has233332h3322ing@s222222222332a87229.dk",
                Role = "Admin"
            };

            var adminPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
             {
                  new Claim(ClaimTypes.Name, userModel.Username),
                  new Claim(ClaimTypes.Role, "Admin")
             }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = adminPrincipal }
            };

            var newuser = controller.CreateUser(userModel);
            var result = controller.GetUser(userModel.Id) as ObjectResult;

            // Assert
            Assert.Equal(newuser, result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Service.DeleteUser(userModel.Id);
        }




        //var newuser = controller.CreateUser(userModel);
        //var result = controller.GetUser(userModel.Id) as ObjectResult;
        ////var createdUser = Service.GetUser(userModel.Id);
        //Assert.Equal(newuser, result);
        //Assert.IsType<ObjectResult>(result);
        //Assert.Equal(200, result.StatusCode);
        ////Assert.Equal(result, createdUser);
        //Service.DeleteUser(userModel.Id);

      }
    }

