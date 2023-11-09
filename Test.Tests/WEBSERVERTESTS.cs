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
        // ... (other test methods)

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
                Id = 40,
                Username = "Hashing5",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Dob = "2020-20-20",
                Email = "hashing@salt5.dk"
            };

            var result = controller.SignIn(userModel) as ObjectResult;


            var createdUser = Service.GetUserByUsername(userModel.Username);
            Assert.NotNull(createdUser);
            Assert.Equal(userModel.Username, createdUser.Username);
            Assert.Equal(userModel.FirstName, createdUser.FirstName);
            Assert.Equal(userModel.LastName, createdUser.LastName);
            Assert.Equal(userModel.Dob, createdUser.Dob);
            Assert.Equal(userModel.Email, createdUser.Email);
            Assert.NotNull(createdUser.Salt);
            Assert.NotNull(createdUser.Password);
            Assert.Equal(userModel.Password, createdUser.Password);
            Service.DeleteUser(createdUser.Id);
            Console.WriteLine(userModel.Password, createdUser.Password);
        }
    }

}