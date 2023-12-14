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

        int CreateUser(User user);
       void CreateUserFromStrings(string username, string password, string firstname, string lastname, string email, string dob, string salt, string role);
        bool DeleteUser(int u_id);
        bool UpdateUser(User user);
        public bool IsEmailAlreadyRegistered(string email);
    public bool IsValidEmail(string email);
    }
