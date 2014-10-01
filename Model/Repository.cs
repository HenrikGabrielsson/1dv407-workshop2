using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class Repository
    {
        private string path;

        public Repository() {
           Path = "users.txt";
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

        public void SaveToFile(User user)
        {
            List<User> users = Load();

            foreach (User listedUser in users)
            {
                while(listedUser.UniqueKey == user.UniqueKey)
                {
                    user.UniqueKey = user.GenerateUniqueKey();
                }
            }

            using (StreamWriter writer = new StreamWriter(Path, true, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("{0};{1};{2};", user.UniqueKey, user.Name, user.PersonalNum);
            }
        }

        public void SaveAllToFile(List<User> users)
        {

        
            using (StreamWriter writer = new StreamWriter(Path, false, System.Text.Encoding.UTF8))
            {
                foreach (User user in users)
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
