using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class UserList
    {
        private List<User> users;

        public UserList()
        {
            this.users = new List<User>();
        }

        public void Create(User user)
        {
            this.users.Add(user);
        }

        public void Delete(User user)
        {
            this.users.Remove(user);
        }

        public List<User> GetUsers()
        {
            return this.users;
        }
    }
}
