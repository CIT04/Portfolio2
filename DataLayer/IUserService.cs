using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



    public interface IUserService

{
        User GetUserByUsername(string username);

        User? GetUser(int id);

        User? GetUserByEmail(string email);

        (IList<User> products, int count) GetUsers(int page, int pageSize);

        void CreateUser(User user);
       void CreateUserFromStrings(string username, string password, string firstname, string lastname, string email, string dob, string salt, string role);
        void DeleteUser(int u_id);
        bool UpdateUser(User user);
    }
