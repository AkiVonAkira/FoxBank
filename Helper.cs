﻿namespace FoxBank
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
                int from_accountId = accounts[index].id;
                Console.WriteLine($"\nYou selected {myArray[index]}.");
                Console.Clear();
                Console.WriteLine("Enter amount to transfer: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("You did not enter a valid input");

                }
                Console.Clear();
                transactionMenu.PrintMenu();
                int index2 = transactionMenu.UseMenu();
                int to_accountId = accounts[index2].id;
                if (from_accountId == to_accountId)
                {
                    Console.WriteLine("Can't Transfer to same account! Try again");
                    Menu.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                else
                {

                    Console.WriteLine($"\nYou selected {myArray[index2]}.");
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
                    }
                }
            }
        }
    }
}
