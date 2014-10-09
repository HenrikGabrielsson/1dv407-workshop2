using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1dv407_workshop2.Model;

namespace _1dv407_workshop2.View
{
    class ConsoleView
    {
        //private UserList list = new UserList();
        private UserRepository repo;
        private BoatRepository boatRepo;
        private List<User> users;
        private List<Boat> boats;

        public ConsoleView(UserRepository repo, BoatRepository boatRepo)
        {
            this.repo = repo;
            this.boatRepo = boatRepo;
            LoadLists();
        }

        private void LoadLists()
        {
            this.users = repo.Load();
            this.boats = boatRepo.Load();
        }

        private void AddUser()
        {
            Console.WriteLine("Ge mig namnet:");

            string name = Console.ReadLine();

            Console.WriteLine("Ge mig personnumret:");
            string personalNum = Console.ReadLine();

            try
            {
                // uniqueKey test. 
                //User u = new User(9351946, name, personalNum);
                User u = new User(name, personalNum);
                this.repo.SaveToFile(u);
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

            LoadLists();
        }

        private void UpdateUser()
        {
            ShowUserList();

            Console.WriteLine("Välj medlem att ändra");
            User user = ChooseUser();

            Console.Write("Namn: ");
            string name = Console.ReadLine();

            Console.Write("Personnummer: ");
            string personalNum = Console.ReadLine();

           
            user.Name = name;
            user.PersonalNum = personalNum;

            this.repo.SaveAllToFile(this.users);


            LoadLists();
        }

        private void RemoveUser()
        {
            ShowUserList();

            Console.WriteLine("Välj medlem att ta bort");

            this.users.Remove(ChooseUser());

            this.repo.SaveAllToFile(this.users);

            LoadLists();
        }

        private User ChooseUser()
        {
            int user;

            int.TryParse(Console.ReadLine(), out user);

            return this.users.Find(item => item.UniqueKey == user);
        }

        private void ShowUserList()
        {

            foreach (User user in this.users)
            {
                int i = 0;
                foreach (Boat boat in this.boats)
                {
                    if (boat.Owner == user.UniqueKey)
                    {
                        i++;
                    }
                }

                Console.WriteLine("Namn: {0}", user.Name);
                Console.WriteLine("Medlemsnummer: {0}", user.UniqueKey);
                Console.WriteLine("Antal båtar: {0}", i);
                Console.WriteLine();
            }
        }

        private void ShowFullUserList()
        {
            foreach (User user in this.users)
            {

                Console.WriteLine("Namn: {0}", user.Name);
                Console.WriteLine("Medlemsnummer: {0}", user.UniqueKey);
                Console.WriteLine("Personnummer: {0}", user.PersonalNum);
                Console.WriteLine();

                foreach (Boat boat in this.boats)
                {
                    if (boat.Owner == user.UniqueKey)
                    {
                        Console.WriteLine("Båtlängd: {0}", boat.Length);
                        Console.WriteLine("Båttyp: {0}", boat.Type);
                        Console.WriteLine("Båtägare: {0}", boat.Owner);
                        Console.WriteLine("Båtnummer: {0}", boat.UniqueId);
                        Console.WriteLine();
                    }
                }

            }
            
            

        }

        private void ShowUser() {
            ShowUserList();

            //välj person
            Console.WriteLine("Medlem du vill visa:");

            User user = ChooseUser();
            Console.WriteLine("Namn: {0},\nMedlemsnummer:{1},\nPersonnummer: {2}", user.Name, user.UniqueKey, user.PersonalNum);

            Console.WriteLine("\nPersonen äger följande båtar:");
            //hämta medlemmens båtar och skriv ut info.
            foreach (Boat boat in this.boats)
            {
                if (boat.Owner == user.UniqueKey)
                {
                    Console.WriteLine("Id: {0}, \nTyp: {1}, \nLängd: {2} cm \n\n", boat.UniqueId, boat.Type, boat.Length);
                }
            }


        }

        private void AddBoat()
        {

            ShowUserList();

            int length;
            int type;
            int i = 0;

            Console.WriteLine("Välj medlem att lägga till båt till");

            User user = ChooseUser();
            Console.WriteLine("Längd:");

            int.TryParse(Console.ReadLine(), out length);

            Console.WriteLine("Välj båttyp: ");
            Console.WriteLine();

            var values = Enum.GetValues(typeof(BoatType));

            foreach (var value in values)
            {
                i++;
                Console.WriteLine("{0}: {1}", i, value);
            }

            int.TryParse(Console.ReadLine(), out type);

            BoatType foo = (BoatType)type;

            Boat boat = new Boat(foo, length, user.UniqueKey);

            this.boatRepo.SaveToFile(boat);

            LoadLists();

        }

        private void RemoveBoat()
        {
            ShowFullUserList();

            int removeBoatNumber;

            Console.WriteLine("Välj båt att ta bort");

            int.TryParse(Console.ReadLine(), out removeBoatNumber);

            this.boats.Remove(this.boats.Find(item => item.UniqueId == removeBoatNumber));

            this.boatRepo.SaveAllToFile(this.boats);

            LoadLists();
        }

        private void UpdateBoat()
        {
            int i = 0;
            int type;
            int length;

            ShowFullUserList();

            int updateBoatNumber;

            Console.WriteLine("Välj båt att ändra");

            int.TryParse(Console.ReadLine(), out updateBoatNumber);

            Boat boat = this.boats.Find(item => item.UniqueId == updateBoatNumber);

            Console.WriteLine("Välj båttyp: ");
            Console.WriteLine();

            var values = Enum.GetValues(typeof(BoatType));

            foreach (var value in values)
            {
                i++;
                Console.WriteLine("{0}: {1}", i, value);
            }

            int.TryParse(Console.ReadLine(), out type);

            Console.Write("Längd: ");
            int.TryParse(Console.ReadLine(), out length);

            BoatType foo = (BoatType)type;

            boat.Type = foo;
            boat.Length = length;

            this.boatRepo.SaveAllToFile(this.boats);

            LoadLists();
        }


        public bool Menu()
        {
            bool exit = false;

            switch (GetMenuChoice())
            {
                case 0:
                    exit = true;
                    break;
                case 1:
                    AddUser();
                    break;
                case 2:
                    UpdateUser();
                    break;
                case 3:
                    RemoveUser();
                    break;
                case 4:
                    ShowUser();
                    break;
                case 5:
                    ShowUserList();
                    break;
                case 6:
                    ShowFullUserList();
                    break;
                case 7:         
                    AddBoat();
                    break;
                case 8:
                    RemoveBoat();
                    break;
                case 9:
                    UpdateBoat();
                    break;
                default:
                    break;
            }

            ContinueOnKeyPressed();

            return exit;
        }

        private int GetMenuChoice()
        {
            int index;

            do
            {
                // Output everything.
                Console.WriteLine("\n - Arkiv ------------------------------\n");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine("\n - Medlem -----------------------------\n");
                Console.WriteLine(" 1. Lägg till medlem.");
                Console.WriteLine(" 2. Ändra medlem.");
                Console.WriteLine(" 3. Ta bort medlem.");
                Console.WriteLine("\n - Lista medlemmar---------------------\n");
                Console.WriteLine(" 4. Lista en medlem.");
                Console.WriteLine(" 5. Lista alla medlemmar.");
                Console.WriteLine(" 6. Lista alla medlemmar(Fullständig).");
                Console.WriteLine("\n - Båt --------------------------------\n");
                Console.WriteLine(" 7. Lägg till båt.");
                Console.WriteLine(" 8. Ta bort båt.");
                Console.WriteLine(" 9. Ändra båt.");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write(" Ange menyval [0-9]: ");
                Console.ResetColor();
                // Check the choice and return it.
                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 9)
                {
                    return index;
                }
                ContinueOnKeyPressed();
            } while (true);
        }

        private void ContinueOnKeyPressed()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("\n   Tryck tangent för att fortsätta   ");
            Console.ResetColor();

            // Hide the cursor.
            Console.CursorVisible = false;

            // Read the key.
            Console.ReadKey(true);
            Console.Clear();

            // Show the cursor.
            Console.CursorVisible = true;
        }
    }
}
