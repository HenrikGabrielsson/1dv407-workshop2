﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{
    class Repository
    {
        private string path;

        public Repository(string path)
        {
           Path = path;
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

        public void SaveToFile(Object obj)
        {
            List<Object> objects = Load();

            if (obj.GetType() == typeof(Boat))
            {
                Boat typed = (Boat)obj;

                using (StreamWriter writer = new StreamWriter(Path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("{0};{1};{2};{3};", (int)typed.Type, typed.Length, typed.Owner, typed.UniqueId);
                }
            }
            else if(obj.GetType() == typeof(User)) {
                User typed = (User)obj;

                using (StreamWriter writer = new StreamWriter(Path, true, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("{0};{1};{2};", typed.UniqueKey, typed.Name, typed.PersonalNum);
                }
            }
            else {
                throw new ArgumentException();
            }


          
        }



        public List<Object> Load()
        {
            List<Object> boatList = new List<Object>();

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
