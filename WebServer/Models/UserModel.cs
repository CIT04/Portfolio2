namespace WebServer.Models
{
    public class UserModel
    {
        public string Url { get; set; } = string.Empty;
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; } //Date of Birth
        public string Email { get; set; }
        public string Role { get; set; } = "User";
        public string Salt { get;  set; } = string.Empty;
    }
}
