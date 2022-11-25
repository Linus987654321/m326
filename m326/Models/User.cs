using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m326.Models
{
    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }

        public Grid Grid { get; set; }
        public User(int id, Role role, string password)
        {
            Id = id;
            Role = role;
            Password = password;
            Grid = new Grid();
        }
    }
}
