 using DataLayer.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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


        public void CreateUser(User user)
        {
            using var db = new Context();
            var IdCount = db.User.Max(x => x.Id) + 1;
            var xUser = new User
            { 
                Id = IdCount,
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Dob = user.Dob,
                Email = user.Email,
            };
            
            

            db.Database.ExecuteSqlInterpolated($"select insert_user({xUser.Id}, {xUser.Username}, {xUser.Password},{xUser.FirstName},{xUser.LastName},{xUser.Dob},{xUser.Email})");

            db.SaveChanges();
        }

        public bool UpdateUser(User user)
        {
             using var db = new Context();
            var xUser = db.User.FirstOrDefault(u => u.Id == user.Id);

            if (xUser != null) 
            {
                db.Database.ExecuteSqlInterpolated($"select update_user({user.Id}, {user.Username}, {user.Password},{user.FirstName},{user.LastName},{user.Dob},{user.Email})");
                db.SaveChanges();
                return true;
            }
            return false;

        }

        //TODO: Add "User not found" on invalid input //Test it
        public void DeleteUser(int u_id) 
        {
            using var db = new Context();
            db.Database.ExecuteSqlInterpolated($"select delete_user_by_id({u_id})");
            db.SaveChanges();
        }

    }

}

