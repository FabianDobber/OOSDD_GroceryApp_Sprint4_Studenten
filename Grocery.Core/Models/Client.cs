
namespace Grocery.Core.Models
{
    public enum Role
    {
        None,
        Admin
    }
    public partial class Client : Model
    {

        public Role Role { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Client(int id, string name, string emailAddress, string password, Role role = Role.None) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
            Role = role;
        }
    }
}
