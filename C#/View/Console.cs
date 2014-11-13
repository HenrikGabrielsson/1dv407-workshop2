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

        private IRepository<User> repo;
        private IRepository<Boat> boatRepo;

        public ConsoleView(UserRepository repo, BoatRepository boatRepo)
        {
            this.repo = repo;
            this.boatRepo = boatRepo;
        }

        //adds member
        private void AddUser()
        {
            //get input from user
            Console.WriteLine("Ge mig namnet:");
            string name = Console.ReadLine();

            Console.WriteLine("Ge mig personnumret:");
            string personalNum = Console.ReadLine();

            //create member and save
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

        //update user
        private void UpdateUser()
        {

            //show list of users to choose from
            ShowUserList();

            //get input from user
            Console.WriteLine("Välj medlem att ändra");
            User user = ChooseUser();

            Console.Write("Namn: ");
            string name = Console.ReadLine();

            Console.Write("Personnummer: ");
            string personalNum = Console.ReadLine();

            //update member
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
        //pick and remove a user
        private void RemoveUser()
        {
            ShowUserList();

            Console.WriteLine("Välj medlem att ta bort");

            try
            {
                this.repo.Remove(ChooseUser());
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

        }

        //a function that let's the user chose a user from the files and return it
        //@return User = chosen user
        private User ChooseUser()
        {
            int user;

            int.TryParse(Console.ReadLine(), out user);

            return repo.Find(user);
        }

        //displays a list of users
        //@param full = the list is shown with all available data + info about their boats.
        private void ShowUserList(bool full = false)
        {
            //go through all the members.
            foreach (User user in this.repo.Load())
            {
                int i = 0;
                //go through all the boats.
                foreach (Boat boat in this.boatRepo.Load())
                {

                    if (boat.Owner.UniqueKey == user.UniqueKey)
                    {
                        if (full)
                        {
                            Console.WriteLine("=========== BÅTAR SOM TILLHÖR: {0} ===========", user.Name);
                            Console.WriteLine("Båtlängd: {0}", boat.Length);
                            Console.WriteLine("Båttyp: {0}", boat.Type);
                            Console.WriteLine("Båtägare: {0}", boat.Owner.UniqueKey);
                            Console.WriteLine("Båtnummer: {0}", boat.UniqueId);
                            Console.WriteLine();
                        }
                        i++;
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

        //pick and show one user
        private void ShowUser() {
            ShowUserList();

            //choose user
            Console.WriteLine("Medlem du vill visa:");

            User user = ChooseUser();
            Console.WriteLine("Namn: {0},\nMedlemsnummer:{1},\nPersonnummer: {2}", user.Name, user.UniqueKey, user.PersonalNum);

            Console.WriteLine("\nPersonen äger följande båtar:");
            
            //get the members boats and print them too.
            foreach (Boat boat in this.boatRepo.Load())
            {
                if (boat.Owner.UniqueKey == user.UniqueKey)
                {
                    Console.WriteLine("Id: {0}, \nTyp: {1}, \nLängd: {2} cm \n\n", boat.UniqueId, boat.Type, boat.Length);
                }
            }


        }

        //Choose a boat and return 
        //@return Boat = chosen boat
        private Boat ChooseBoat()
        {
            int boat;
            int.TryParse(Console.ReadLine(), out boat);

            return boatRepo.Find(boat);
        }

        //add boat
        private void AddBoat()
        {
            //show the list of users so one can chose an owner
            ShowUserList();

            int length;
            int type;
            int i = 0;

            //get input
            Console.WriteLine("Välj medlem att lägga till båt till");
            User user = ChooseUser();
            
            Console.WriteLine("Längd:");
            int.TryParse(Console.ReadLine(), out length);

            Console.WriteLine("Välj båttyp: ");
            Console.WriteLine();
            var values = Enum.GetValues(typeof(BoatType));

            //print a list of all possible boattypes
            foreach (var value in values)
            {
                i++;
                Console.WriteLine("{0}: {1}", i, value);
            }
            int.TryParse(Console.ReadLine(), out type);
            BoatType foo = (BoatType)type;

            //create a boat!
            try
            {
                Boat boat = new Boat(foo, length, user, this.boatRepo.GetUniqueId());

                this.boatRepo.Add(boat);
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }

        }


        //choose and remove a boat from file.
        private void RemoveBoat()
        {
            ShowUserList(true);

            Console.WriteLine("Välj båt att ta bort");

            try
            {
                this.boatRepo.Remove(ChooseBoat());
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }
        }


        //choose and update the info of a boat.
        private void UpdateBoat()
        {
            int i = 0;
            int type;
            int length;

            ShowUserList(true);

            //get input 
            Console.WriteLine("Välj båt att ändra");
            Boat boat = ChooseBoat();

            Console.WriteLine("Välj båttyp: ");
            Console.WriteLine();
            var values = Enum.GetValues(typeof(BoatType));

            //pritn boattypes
            foreach (var value in values)
            {
                i++;
                Console.WriteLine("{0}: {1}", i, value);
            }
            int.TryParse(Console.ReadLine(), out type);

            Console.Write("Längd: ");
            int.TryParse(Console.ReadLine(), out length);

            BoatType boattype = (BoatType)type;

            //update boat
            try
            {
                boat.Type = boattype;
                boat.Length = length;

                this.boatRepo.Update();
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }
        }

        //check what the user wants to do and then call a function that does just that.
        //@return bool = true: user wants to quit. false: User wants to go again
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

        //function that prints the menu and lets user pick an option from the list.
        //@return int = chosen option from list.
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

        //stops the program until user presses a key.
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
