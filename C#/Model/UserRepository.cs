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
            //paths to the files
            Path = "users.txt";
            IdPath = "userId.txt";

            //load the list of users and keep in a list.
            this.users = Load();
        }

        //properties.
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

        //find a specified user
        //@param id = unique id of the user to be found
        //@return User = the found user is returned.
        public User Find(int id)
        {
            return this.users.Find(item => item.UniqueKey == id);
        }

        //remove specified user
        //@param user = Unique id of the user to be removed.
        public void Remove(User user)
        {
            if (this.users.Remove(user) == false)
            {
                throw new Exception("FEL");
            }
            SaveAllToFile();
        }

        //update file with new data.
        public void Update()
        {
            SaveAllToFile();
        }

        //add user
        //@param user = user to be added.
        public void Add(User user)
        {
            this.users.Add(user);
            SaveAllToFile();
        }

        //get a unique id by checking the last given unique id. 
        public int GetUniqueId()
        {
            // Encoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            int id;

            //read id
            using (StreamReader reader = new StreamReader(IdPath, enc))
            {
                int.TryParse(reader.ReadLine(), out id);
            }

            //update id in file
            using (StreamWriter writer = new StreamWriter(IdPath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(++id);
            }

            return id;
        }

        //save the list to file, after changes has been made.
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


        //Load the users in the file to a list. 
        //@return List<User> = the list with the users.
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
            }


            // ... and return the list.
            return userList;
        }
    }
}
