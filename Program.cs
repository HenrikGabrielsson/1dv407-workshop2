using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1dv407_workshop2.View;
using _1dv407_workshop2.Model;

namespace _1dv407_workshop2
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();
            BoatRepository boatRepo = new BoatRepository();
            ConsoleView cv = new ConsoleView(repo, boatRepo);

            while(cv.Menu());

        }
    }
}
