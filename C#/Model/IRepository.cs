using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1dv407_workshop2.Model
{

    //this is what a repository should look like.
    interface IRepository<T>
    {
        List<T> Load();

        void SaveAllToFile();

        void Remove(T obj);

        T Find(int id);

        void Update();

        int GetUniqueId();

        void Add(T obj);
    }
}
