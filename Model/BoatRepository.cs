using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class BoatRepository
    {
        private string path;

        public BoatRepository()
        {
           Path = "boats.txt";
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

        public void SaveToFile(Boat boat)
        {
            List<Boat> boats = Load();

            foreach (Boat listedBoat in boats)
            {
                while (listedBoat.UniqueId == boat.UniqueId)
                {
                    boat.UniqueId = boat.GenerateUniqueKey();
                }
            }


            using (StreamWriter writer = new StreamWriter(Path, true, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("{0};{1};{2};{3};", (int)boat.Type, boat.Length, boat.Owner, boat.UniqueId);
            }
        }

        public void SaveAllToFile(List<Boat> boats)
        {


            using (StreamWriter writer = new StreamWriter(Path, false, System.Text.Encoding.UTF8))
            {
                foreach (Boat boat in boats)
                {
                    writer.WriteLine("{0};{1};{2};{3};", (int)boat.Type, boat.Length, boat.Owner, boat.UniqueId);
                }

            }
        }


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

                    Boat boat = new Boat(foo, int.Parse(boats[1]), int.Parse(boats[2]), int.Parse(boats[3]));

                    // Add it to the list.
                    boatList.Add(boat);

                }
            }


            // ... and return the list.
            return boatList;
        }
    }
}
