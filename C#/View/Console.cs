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

        public ConsoleView(UserRepository repo, BoatRepository boatRepo)
        {
            //Repository<Boat> boats = new Repository<Boat>();
            this.repo = repo;
            this.boatRepo = boatRepo;
        }


        private void AddUser()
        {
            Console.WriteLine("Ge mig namnet:");

            string name = Console.ReadLine();

            Console.WriteLine("Ge mig personnumret:");
            string personalNum = Console.ReadLine();

            try
            {
                User u = new User(this.repo.GetUniqueId(), name, personalNum);
                this.repo.Add(u);
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

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


            try
            {
                user.Name = name;
                user.PersonalNum = personalNum;

                this.repo.Update();
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

        }

        private void RemoveUser()
        {
            ShowUserList();

            Console.WriteLine("Välj medlem att ta bort");

            this.repo.Remove(ChooseUser());

        }

        private User ChooseUser()
        {
            int user;

            int.TryParse(Console.ReadLine(), out user);

            return repo.Find(user);
        }

        private void ShowUserList(bool full = false)
        {


            foreach (User user in this.repo.Load())
            {
                int i = 0;
                foreach (Boat boat in this.boatRepo.Load())
                {
                    if (boat.Owner == user.UniqueKey)
                    {
                        if (full)
                        {
                            Console.WriteLine("=========== BÅTAR SOM TILLHÖR: {0} ===========", user.Name);
                            Console.WriteLine("Båtlängd: {0}", boat.Length);
                            Console.WriteLine("Båttyp: {0}", boat.Type);
                            Console.WriteLine("Båtägare: {0}", boat.Owner);
                            Console.WriteLine("Båtnummer: {0}", boat.UniqueId);
                            Console.WriteLine();
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

                Console.WriteLine("Namn: {0}", user.Name);
                Console.WriteLine("Medlemsnummer: {0}", user.UniqueKey);
                Console.WriteLine("Personnummer: {0}", user.PersonalNum);
                Console.WriteLine("Antal båtar: {0}", i);
                Console.WriteLine("======================");
                Console.WriteLine();
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
            foreach (Boat boat in this.boatRepo.Load())
            {
                if (boat.Owner == user.UniqueKey)
                {
                    Console.WriteLine("Id: {0}, \nTyp: {1}, \nLängd: {2} cm \n\n", boat.UniqueId, boat.Type, boat.Length);
                }
            }


        }

        private Boat ChooseBoat()
        {
            int boat;

            int.TryParse(Console.ReadLine(), out boat);

            return boatRepo.Find(boat);
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

            try
            {
                Boat boat = new Boat(foo, length, user.UniqueKey, this.boatRepo.GetUniqueId());

                this.boatRepo.Add(boat);
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

        }

        private void RemoveBoat()
        {
            ShowUserList(true);

            Console.WriteLine("Välj båt att ta bort");

            this.boatRepo.Remove(ChooseBoat());
        }

        private void UpdateBoat()
        {
            int i = 0;
            int type;
            int length;

            ShowUserList(true);

            Console.WriteLine("Välj båt att ändra");

            Boat boat = ChooseBoat();

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

            try
            {
                boat.Type = foo;
                boat.Length = length;

                this.boatRepo.Update();
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }
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
                    ShowUserList(true);
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
