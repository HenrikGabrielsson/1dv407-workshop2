using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class UserRepository : IRepository<User>
    {
        private string path;
        private string idPath;
        private List<User> users;

        public UserRepository() {
            Path = "users.txt";
            IdPath = "userId.txt";
            this.users = Load();
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                if (value == null || String.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("Path is missing!");
                }
                this.path = value;
            }
        }

        public string IdPath
        {
            get
            {
                return this.idPath;
            }
            set
            {
                if (value == null || String.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("Path is missing!");
                }
                this.idPath = value;
            }
        }

        public User Find(int id)
        {
            return this.users.Find(item => item.UniqueKey == id);
        }

        public void Remove(User user)
        {
            this.users.Remove(user);
            SaveAllToFile();
        }

        public void Update()
        {
            SaveAllToFile();
        }

        public void Add(User user)
        {
            this.users.Add(user);
            SaveAllToFile();
        }

        public int GetUniqueId()
        {
            // Encoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            int id;

            using (StreamReader reader = new StreamReader(IdPath, enc))
            {
                int.TryParse(reader.ReadLine(), out id);
            }

            using (StreamWriter writer = new StreamWriter(IdPath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(++id);
            }

            return id;
        }


        public void SaveAllToFile()
        {
            using (StreamWriter writer = new StreamWriter(Path, false, System.Text.Encoding.UTF8))
            {
                foreach (User user in this.users)
                {
                    writer.WriteLine("{0};{1};{2};", user.UniqueKey, user.Name, user.PersonalNum);
                }
                
            }
        }





        public List<User> Load()
        {
            List<User> userList = new List<User>();

            // Encoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            using (StreamReader reader = new StreamReader(Path, enc))
            {
                string line = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line == String.Empty)
                    {
                        continue;
                    }

                    // Remove all the semicolons.
                    string[] users = line.Split(';');

                    if (users.Length != 4)
                    {
                        throw new ApplicationException("Wrong formatted file!");
                    }

                    User user = new User(int.Parse(users[0]), users[1], users[2]);
    
                    // Add it to the list.
                    userList.Add(user);

                }
                userList.Sort();
            }


            // ... and return the list.
            return userList;
        }
    }
}
