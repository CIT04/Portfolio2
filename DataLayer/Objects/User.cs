using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Objects
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Username { get; set; } = string.Empty;
        //public string Password { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
    }
}
