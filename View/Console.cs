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
        private Repository repo;
        private List<User> users;

        public ConsoleView(Repository repo)
        {
            this.repo = repo;
            this.users = repo.Load();
        }

        private void AddUser()
        {
            Console.WriteLine("Ge mig namn");

            string name = Console.ReadLine();
            string personalNum = Console.ReadLine();

            try
            {
                // uniqueKey test. 
                //User u = new User(9351946, name, personalNum);
                User u = new User(name, personalNum);
                this.repo.SaveToFile(u);

                PresentUser(u);
            }
            catch (Exception)
            {
                Console.WriteLine("FEL");
            }
        }

        private void RemoveUser()
        {
            ShowUserList();

            int removeUserNumber;

            Console.WriteLine("Välj medlem att ta bort");

            int.TryParse(Console.ReadLine(), out removeUserNumber);

            User user = this.users.Find(item => item.UniqueKey == removeUserNumber);

            this.users.Remove(user);

            this.repo.SaveAllToFile(this.users);
        }

        private void ShowUserList()
        {
            foreach (User user in this.users)
            {
                Console.WriteLine("Namn: {0}", user.Name);
                Console.WriteLine("Medlemsnummer: {0}", user.UniqueKey);
                Console.WriteLine();
            }
        }

        private void PresentUser(User user) {
            Console.WriteLine(user.Name);

        }


        public void Menu()
        {
            bool exit = false;

            do
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        exit = true;
                        continue;
                        break;
                    case 1:
                        AddUser();
                        break;
                    case 2:
                        break;
                    case 3:
                        RemoveUser();
                        break;
                    case 4:
                        break;
                    case 5:
                        ShowUserList();
                        break;
                    default:
                        break;
                }
                ContinueOnKeyPressed();
            } while (exit);
        }

        private static int GetMenuChoice()
        {
            int index;

            do
            {
                // Output everything.
                Console.WriteLine("\n - Arkiv ------------------------------\n");
                Console.WriteLine(" 0. Avsluta.");
                Console.WriteLine(" 1. Lägg till medlem.");
                Console.WriteLine("\n - Redigera ---------------------------\n");
                Console.WriteLine(" 2. Ändra medlem.");
                Console.WriteLine(" 3. Ta bort medlem.");
                Console.WriteLine("\n - Lista ------------------------------\n");
                Console.WriteLine(" 4. Lista en medlem.");
                Console.WriteLine(" 5. Lista alla medlemmar.");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write(" Ange menyval [0-5]: ");
                Console.ResetColor();
                // Check the choice and return it.
                if (int.TryParse(Console.ReadLine(), out index) && index >= 0 && index <= 5)
                {
                    return index;
                }
                ContinueOnKeyPressed();
            } while (true);
        }

        // Private static method for pressing any key to continue.
        private static void ContinueOnKeyPressed()
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
