using DataLayer;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebServer.Controllers;

namespace Test.Tests;


public class MediaTest
{
    private const string MediaApi = "http://localhost:5001/api/media";
    

    /* /api/categories */

}

public class UserTest
{
    private const string UserApi = "http://localhost:5001/api/user";
    //TEST CREATE USER WORKS WITH VALID INPUT
    [Fact]
    public void CreateUser_ValidData_CreatesUserAndReturnsNewObject()
    {
        var service = new UserService();
        var userToCreate = new DataLayer.Objects.User()
        { 
            Username = "Mor4",
            Password = "123456789",
            FirstName= "Ulla",
            LastName ="Terkelsen",
            Dob = "1979-10-10",
            Email= "Jegelsker@fødder4.dk"
        };

       var createdId = service.CreateUser(userToCreate);
        var newcreated = service.GetUser(createdId);

        Assert.NotNull(newcreated);
        Assert.Equal("Mor4", newcreated.Username);
        Assert.Equal("1979-10-10", newcreated.Dob);
        service.DeleteUser(createdId);
     
    }


    [Fact]
    public void UpdateUser_ValidData_UpdatesUserProperties()
    {
        var service = new UserService();
        var userToCreate2 = new DataLayer.Objects.User()
        {
            Username = "UpdateTest3",
            Password = "password",
            FirstName = "Per",
            LastName = "Hansen",
            Dob = "1985-05-20",
            Email = "vent@komnu3.dk"
        };

        var createdId2 = service.CreateUser(userToCreate2);
        var userToUpdate = service.GetUser(createdId2);

        userToUpdate.FirstName = "UPDATENAVN";
        userToUpdate.LastName = "EFTERNAVN";
        userToUpdate.Email = "Opdateret@email.dk";

        var updateResult = service.UpdateUser(userToUpdate);

        var updatedUser = service.GetUser(createdId2);

        Assert.True(updateResult);
        Assert.NotNull(updatedUser);
        Assert.Equal("UPDATENAVN", updatedUser.FirstName);
        Assert.Equal("EFTERNAVN", updatedUser.LastName);
        Assert.Equal("Opdateret@email.dk", updatedUser.Email);

        service.DeleteUser(createdId2);
    }

    // TEST UPDATE USER WITH INVALID ID RETURNS FALSE
    [Fact]
    public void UpdateUser_InvalidId_ReturnsFalse()
    {
        var service = new UserService();
        var invalidUser = new DataLayer.Objects.User
        {
            Id = -1, // Invalid ID
            Username = "JegVirkerIkke",
            Password = "password",
            FirstName = "Invalid",
            LastName = "User",
            Dob = "2020-10-20",
            Email = "JegVirker@Ikke.dk"
        };

        var updateResult = service.UpdateUser(invalidUser);

        Assert.False(updateResult);
    }



    //[Fact]
    //public async Task ApiUsers_PostWithUser_Created()
    //{
    //    var newUser = new
    //    {
    //        Username = "Mor",
    //        Password = "123456789",
    //        FirstName = "Ulla",
    //        LastName = "Terkelsen",
    //        Dob = "1979-10-10",
    //        Email = "Jegelsker@fødder.dk"
    //    };
    //    var (user, statusCode) = await PostData(UserApi, newUser);

    //    string? id = null;
    //    if (user?.Value("id") == null)
    //    {
    //        var url = user?.Value("url");
    //        if (url != null)
    //        {
    //            id = url.Substring(url.LastIndexOf('/') + 1);
    //        }
    //    }
    //    else
    //    {
    //        id = user.Value("id");
    //    }

    //    Assert.Equal(HttpStatusCode.Created, statusCode);

    //    await DeleteData($"{UserApi}/{id}");
    //}
    //TESTS FOR LOCALRATING/USER


    //[Fact]
    //public void CreateUser_InValidData_CreatesUserAndThrowsExceptionForDuplicateUser()
    //{
    //    var service = new UserService();
    //    var userToCreate2 = new DataLayer.Objects.User
    //    {
    //        Username = "Test1&2",
    //        Password = "1234",
    //        FirstName = "Ulla",
    //        LastName = "Terkelsen",
    //        Dob = "1979-10-10",
    //        Email = "Ulla@terkelsen.dk"
    //    };
    //    var ex = Assert.Throws<Npgsql.PostgresException>(() => service.CreateUser(userToCreate2));
    //    Assert.Equal("P0001: Username or email already registered", ex.Message);
    //}









    // Helpers

    async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
    {
        var client = new HttpClient();
        var requestContent = new StringContent(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");
        var response = await client.PostAsync(url, requestContent);
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<HttpStatusCode> PutData(string url, object content)
    {
        var client = new HttpClient();
        var response = await client.PutAsync(
            url,
            new StringContent(
                JsonSerializer.Serialize(content),
                Encoding.UTF8,
                "application/json"));
        return response.StatusCode;
    }

    async Task<HttpStatusCode> DeleteData(string url)
    {
        var client = new HttpClient();
        var response = await client.DeleteAsync(url);
        return response.StatusCode;
    }
}

static class HelperExt
{
    public static string? Value(this JsonNode node, string name)
    {
        var value = node[name];
        return value?.ToString();
    }

    public static string? FirstElement(this JsonArray node, string name)
    {
        return node.First()?.Value(name);
    }

    public static string? LastElement(this JsonArray node, string name)
    {
        return node.Last()?.Value(name);
    }
}
