using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserService : IUserService
    {
        public (IList<User> products, int count) GetUsers(int page, int pageSize)
        {
            var db = new Context();
            var user =
                db.User
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return (user, db.User.Count());
        }

        public User? GetUser(int id)
        {
            var db = new Context();
            return db.User.FirstOrDefault(x => x.Id == id);
        }

        public void CreateUser(string Username)
        {
            //using var db = new Context();
            //var IdCount = db.User.Max(x => x.Id) + 1;

            //var id = IdCount;
            //var username = Username;

            //db.Database.ExecuteSqlInterpolated($"select insert_user({id}, {username})");

            //db.SaveChanges();
        }


    }

}

