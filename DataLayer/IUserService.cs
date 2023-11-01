using DataLayer.Models;
using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace DataLayer;



    public interface IUserService

{ 
        User? GetUser(int id);

        (IList<User> products, int count) GetUsers(int page, int pageSize);

        void CreateUser(User user);
        void DeleteUser(int u_id);
        bool UpdateUser(User user);
    }
