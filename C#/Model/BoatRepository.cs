using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class BoatRepository : IRepository<Boat>
    {
        private string path;
        private string idPath;
        private List<Boat> boats;
        private UserRepository userRepo;
        private List<User> users;

        
        public BoatRepository()
        {
           //set file paths.
           Path = "boats.txt";
           IdPath = "boatId.txt";

            //a list with the boats is read from file.
           this.userRepo = new UserRepository();
           this.users = this.userRepo.Load();
           this.boats = Load();
        }

        
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                //throw exception if no file path is chosen.
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
                //throw exception if no file path is chosen.
                if (value == null || String.IsNullOrWhiteSpace(value))
                {
                    throw new ApplicationException("Path is missing!");
                }
                this.idPath = value;
            }
        }

        //function that checks the last used Unique ID number, and returns the next in line.
        public int GetUniqueId()
        {
            // Encoding
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(1252);

            int id;

            //get number.
            using (StreamReader reader = new StreamReader(IdPath, enc))
            {
                int.TryParse(reader.ReadLine(), out id);
            }

            //update the number in the file.
            using (StreamWriter writer = new StreamWriter(IdPath, false, System.Text.Encoding.UTF8))
            {
                writer.WriteLine(++id);
            }

            return id;
        }

        //Find a specific boat
        //@param id = the unique id of the boat.
        public Boat Find(int id)
        {
           return this.boats.Find(item => item.UniqueId == id);
        }

        //remove a boat
        //@param boat = the boat to be removed
        public void Remove(Boat boat)
        {
            if (this.boats.Remove(boat) == false)
            {
                throw new Exception("FEL");
            }
            SaveAllToFile();
        }

        //update the list in the file.
        public void Update()
        {
            SaveAllToFile();
        }

        //add a given boat to file.
        //@param boat = the boat to be added.
        public void Add(Boat boat)
        {
            this.boats.Add(boat);
            SaveAllToFile();
        }

        //save the list to file (probably after some changes has been made)
        public void SaveAllToFile()
        {
            using (StreamWriter writer = new StreamWriter(Path, false, System.Text.Encoding.UTF8))
            {
                foreach (Boat boat in this.boats)
                {
                    writer.WriteLine("{0};{1};{2};{3};", (int)boat.Type, boat.Length, boat.Owner.UniqueKey, boat.UniqueId);
                }

            }
        }

        //get the boats from file and add to list
        //@return List<Boat> = all the boats.
        public List<Boat> Load()
        {

            List<Boat> boatList = new List<Boat>();

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
                    string[] boats = line.Split(';');

                    if (boats.Length != 5)
                    {
                        throw new ApplicationException("Wrong formatted file!");
                    }

                    BoatType foo = (BoatType)int.Parse(boats[0]);


                    int userKey;
                    int.TryParse(boats[2], out userKey);
                    User user = this.users.Find(item => item.UniqueKey == userKey);
                    Boat boat = new Boat(foo, int.Parse(boats[1]), user, int.Parse(boats[3]));

                    // Add it to the list.
                    boatList.Add(boat);

                }
            }


            // ... and return the list.
            return boatList;
        }
    }
}
