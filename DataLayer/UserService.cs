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

        public User GetUserByUsername (string username)
        { 
            var db = new Context();
            return db.User.FirstOrDefault(x => x.Username == username); 
        }

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


        public int CreateUser(User user)
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
                Salt = user.Salt,
                Role = user.Role

            };
            
            

            db.Database.ExecuteSqlInterpolated($"select insert_user({xUser.Id}, {xUser.Username}, {xUser.Password},{xUser.FirstName},{xUser.LastName},{xUser.Dob},{xUser.Email},{xUser.Salt}, {xUser.Role})");
            return xUser.Id;
            db.SaveChanges();
        }

        public bool UpdateUser(User user)
        {
             using var db = new Context();
            var xUser = db.User.FirstOrDefault(u => u.Id == user.Id);

            if (xUser != null) 
            {
                db.Database.ExecuteSqlInterpolated($"select update_user({user.Id}, {user.Username}, {user.Password},{user.FirstName},{user.LastName},{user.Dob},{user.Email},{xUser.Salt}, {xUser.Role})");
                db.SaveChanges();
                return true;
            }
            return false;

        }


        public bool DeleteUser(int u_id) 
        {
            if(u_id != null && u_id > 1) {
                using var db = new Context();
                db.Database.ExecuteSqlInterpolated($"select delete_user_by_id({u_id})");
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public User? GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void CreateUserFromStrings(string username, string password, string firstname, string lastname, string email, string dob, string salt, string role)
        {
            using var db = new Context();
            var IdCount = db.User.Max(x => x.Id) + 1;
            var xUser = new User
            {
                Id = IdCount,
                Username = username,
                Password = password,
                FirstName = firstname,
                LastName = lastname,
                Dob = dob,
                Email = email,
                Salt = salt,
                Role = role
            };



            db.Database.ExecuteSqlInterpolated($"select insert_user({xUser.Id}, {xUser.Username}, {xUser.Password},{xUser.FirstName},{xUser.LastName},{xUser.Dob},{xUser.Email},{xUser.Salt}, {xUser.Role})");

            db.SaveChanges();
        }

        //public void CreateUser(string username, string password, string firstname, string lastname, string email, string dob, string salt)
        //{
        //    using var db = new Context();
        //    var IdCount = db.User.Max(x => x.Id) + 1;
        //    var xUser = new User
        //    {
        //        Id = IdCount,
        //        Username = username,
        //        Password = password,
        //        FirstName = firstname,
        //        LastName = lastname,
        //        Dob = dob,
        //        Email = email,
        //        Salt = salt,
        //    };



        //    db.Database.ExecuteSqlInterpolated($"select insert_user({xUser.Id}, {xUser.Username}, {xUser.Password},{xUser.FirstName},{xUser.LastName},{xUser.Dob},{xUser.Email},{xUser.Salt})");

        //    db.SaveChanges();
        //}
    }

}

