namespace WebServer.Models
{
    public class CreateUserModel
    {

        public int Id { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; } //Date of Birth
        public string Email { get; set; }

    }
}
