using DataLayer.Objects;
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

        public User? GetUser(string id)
        {
            var db = new Context();
            return db.User.FirstOrDefault(x => x.Id == id);
        }

        public void CreateUser(User user)
        {

            var db = new Context();
            var id = db.User.Max(x => x.Id) + 1;
            var newUser = new User
            {
                Id = id.ToString(),
                Username = user.Username,
            };
            db.Add(newUser);
            db.SaveChanges();
        }
            

    }

}

