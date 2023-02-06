namespace FoxBank
{
    public static class Helper
    {
        internal static void WhichAccount()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(Menu.LoggedInUserID);

            string[] myArray = accounts.Select(account => account.name).ToArray();
            Array.Resize(ref myArray, myArray.Length + 1);
            myArray[myArray.Length - 1] = "Back";

            Menu transactionMenu = new Menu(myArray);
            transactionMenu.PrintMenu();
            int index = transactionMenu.UseMenu();

            if (index + 1 == myArray.Length)
            {
                Menu.LoggedInMenu();
            }
            else
            {
                Console.Clear();
                int from_accountId = accounts[index].id;
                Console.WriteLine($"\nYou selected {myArray[index]}.");
                Menu.EnterToContinue();
                Console.Clear();
                transactionMenu.PrintMenu();
                int index2 = transactionMenu.UseMenu();
                if (index2 + 1 == myArray.Length)
                {
                    Menu.LoggedInMenu();
                }
                int to_accountId = accounts[index2].id;
                if (from_accountId == to_accountId)
                {
                    Console.WriteLine("Can't Transfer to same account! Try again");
                    Menu.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                else
                {


                    Console.Clear();
                    Console.WriteLine($"\nYou selected {myArray[index2]}.");
                    Console.WriteLine("Enter amount to transfer: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                    {
                        Console.WriteLine("You did not enter a valid input");
                        Menu.EnterToContinue();
                        Menu.LoggedInMenu();

                    }
                    bool success = PostgresDataAccess.MoneyTransfer(from_accountId, to_accountId, amount);
                    if (success)
                    {
                        Console.WriteLine("Transaction compelete");
                        Menu.EnterToContinue();
                        Menu.LoggedInMenu();
                    }
                    else
                    {
                        Console.WriteLine("Transaction Failed, Not Enough Funds");
                        Menu.EnterToContinue();
                        Menu.LoggedInMenu();
                    }
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

        internal static int ListDBMenuName(string[] array)
        {
            Menu menu = new Menu(array);
            menu.PrintMenu();
            int index = menu.UseMenu();
            return index;
        }
    }
}
