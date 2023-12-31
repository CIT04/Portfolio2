﻿
﻿using DataLayer;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
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
//
public class UserTest
{
    [Fact]
    public void GetUserByUsername_ReturnsUser()
    {   
        var service = new UserService();
        var userToCreate = new DataLayer.Objects.User()
        {
            Username = "Morade4",
            Password = "123456789",
            FirstName = "Ulla",
            LastName = "Terkelsen",
            Dob = "1979-10-10",
            Email = "Jegelskmaer@.dk"
        };
        var newuserid = service.CreateUser(userToCreate);

        var usergot = service.GetUser(newuserid);

        Assert.Equal(usergot.Id,newuserid);
       

        service.DeleteUser(newuserid);
    }

    [Fact]
    public void GetUserByUsername_WithIncorrectUsername_ReturnsNull()
    {
        var service = new UserService();

        var incorrectUsername = "NonExistentUser";
        var nonExistentUser = service.GetUserByUsername(incorrectUsername);

        Assert.Null(nonExistentUser);
    }
    [Fact]
    public void GetUser_WithIncorrectUserId_ReturnsNull()
    {
        var service = new UserService();


        var incorrectUserId = -1; // 
        var nonExistentUserById = service.GetUser(incorrectUserId);

        Assert.Null(nonExistentUserById);
    }


   
    //TEST CREATE USER WORKS WITH VALID INPUT
    [Fact]
    public void CreateUser_ValidData_CreatesUserAndReturnsNewObject()
    {
        var service = new UserService();
        var userToCreate = new DataLayer.Objects.User()
        {
            Username = "jajfisk",
            Password = "123456789",
            FirstName = "Ulla",
            LastName = "Terkelsen",
            Dob = "1979-10-10",
            Email = "Jegelsdsaker@da.dk"
        };
        var createdId = service.CreateUser(userToCreate);
        var newcreated = service.GetUser(createdId);
        Assert.NotNull(newcreated);
        Assert.Equal("jajfisk", newcreated.Username);
        Assert.Equal("1979-10-10", newcreated.Dob);
        service.DeleteUser(createdId);

    }
   

    [Fact]
    public void Delete_user_test()
    {
        var service = new UserService();
        var userToCreate = new DataLayer.Objects.User()
        {
            Username = "Maor4",
            Password = "123456789",
            FirstName = "Ulla",
            LastName = "Terkelsen",
            Dob = "1979-10-10",
            Email = "Jegealsker@.dk"
        };
        var createdId = service.CreateUser(userToCreate);
        var newcreated = service.GetUser(createdId);
        Assert.NotNull(newcreated);
        var del = service.DeleteUser(createdId);
        Assert.True(del);

    }

    [Fact]
    public void DeleteUser_WithNonExistentUser_ReturnsFalse()
    {
        var service = new UserService();

        
        var nonExistentUserId = -1; 
        var deletionResult = service.DeleteUser(nonExistentUserId);

        Assert.False(deletionResult); 
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
