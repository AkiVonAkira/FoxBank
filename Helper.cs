namespace FoxBank
{
    public static class Helper
    {
        internal static void WhichAccount()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(Menu.LoggedInUserID);

            string[] myArray = accounts.Select(account => account.name + ": " + account.balance).ToArray();
            int index = Helper.MenuIndexer(myArray, true);
            if (index == myArray.Length)
            {
                Menu.LoggedInMenu();
            }
            else
            {
                Console.Clear();
                int from_accountId = accounts[index].id;
                Console.WriteLine($"\nYou selected {myArray[index]}.");
                // remove the selected menu item from the array
                myArray = myArray.Where(o => o != myArray[index]).ToArray();
                EnterToContinue();
                int index2 = Helper.MenuIndexer(myArray, true);
                if (index2 == myArray.Length)
                {
                    Menu.LoggedInMenu();
                }
                int to_accountId = accounts[index2].id;

                Console.Clear();
                Console.WriteLine($"\nYou selected {myArray[index2]}.");
                Console.WriteLine("Enter amount to transfer: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("You did not enter a valid input");
                    EnterToContinue();
                    Menu.LoggedInMenu();

                }
                bool success = PostgresDataAccess.MoneyTransfer(from_accountId, to_accountId, amount);
                if (success)
                {
                    Console.WriteLine("Transaction compelete");
                    EnterToContinue();
                    Menu.LoggedInMenu();
                }
                else
                {
                    Console.WriteLine("Transaction Failed, Not Enough Funds");
                    EnterToContinue();
                    Menu.LoggedInMenu();
                }
            }
        }

        internal static string InputStringValidator(string prompt)
        {
            string userInput = "";
            while (userInput.Length == 0)
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
                if (userInput.Length == 0)
                {
                    Console.WriteLine("\nThat is not a valid input. Please try again.");
                }
            }
            return userInput;
        }

        // This snippet returns an index of the selected menu item from
        internal static int MenuIndexer(string[] array, bool hasBack = false)
        {
            if (hasBack)
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = "Go Back";
            }
            Menu menu = new Menu(array);
            menu.PrintMenu();
            int index = menu.UseMenu();
            return index;
        }

        internal static void Delay()
        {
            int delay = 0;
            for (int i = 0; delay < 15; i++)
            {
                delay++;
                Console.Write(".");
                Thread.Sleep(175);
                if (delay > 10)
                {
                    delay++;
                    Console.Write(".");
                    Thread.Sleep(75);
                }
            }
            Console.Write($"\u2713\n");
            Thread.Sleep(600);
        }

        internal static void EnterToContinue()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
