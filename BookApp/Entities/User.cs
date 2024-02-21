using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Entities
{
    public class User
    {
        public int UserId  { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<Role> Roles { get; set; }
        public List<Book> Books { get; set; }

    }
}
