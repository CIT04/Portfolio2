using DataLayer;
using DataLayer.Objects;
using Microsoft.AspNetCore.Mvc;
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
        public void SignIn_ValidUser_CreatesUserAndReturnsOkResult()
        {
            // Arrange
            var Service = new UserService(); // Replace this with your actual UserService implementation
            var hashing = new Hashing(); // Replace this with your actual Hashing implementation

            var controller = new UserController(Service, null, hashing, null);

            var userModel = new User
            {
                Id = 600,
                Username = "Hashing9",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Dob = "2020-20-20",
                Email = "hashing@sa879.dk",
                Role = "Admin"
            };

            var result = controller.SignIn(userModel) as ObjectResult;


            var createdUser = Service.GetUser(userModel.Id);
         
            Assert.Equal(userModel.Password, createdUser.Password);
            Service.DeleteUser(createdUser.Id);
           
        }
    }

}