using FoxBank.Models;

namespace FoxBank
{
    internal class Transaction
    {
        internal static void Transfer()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(Menu.LoggedInUserID);
            string[] myArray = Helper.GetUserAccountInformation(Menu.LoggedInUserID);
            string[] transferMenu = { "Transfer To Self", "Transfer To Other" };
            int index = Helper.MenuIndexer(myArray, true);
            if (index == myArray.Length) { Menu.LoggedInMenu(); }
            else
            {
                AsciiArt.PrintHeader();
                int from_accountId = accounts[index].id;
                string fromTransfer = ($"{myArray[index]}");
                Console.WriteLine($"Transfering From: \n{myArray[index]}");
                // remove the selected menu item from the array
                myArray = myArray.Where(o => o != myArray[index]).ToArray();
                Helper.EnterToContinue();
                int TransferIndex = Helper.MenuIndexer(transferMenu, true, $"Transfering From: {fromTransfer}");
                if (TransferIndex == 0)
                {
                    int index2 = Helper.MenuIndexer(myArray, true, $"Transfering From: {fromTransfer}");
                    if (index2 == myArray.Length)
                    {
                        Menu.LoggedInMenu();
                    }
                    int to_accountId = accounts[index2].id;

                    AsciiArt.PrintHeader();
                    Console.WriteLine($"Transfering From: {fromTransfer}\n\nTransfering To: {myArray[index2]}\n");
                    decimal amount = Helper.InputDecimalValidator("Enter amount to transfer: ");
                    bool success = PostgresDataAccess.MoneyTransfer(from_accountId, to_accountId, amount);
                    if (success)
                    {
                        Console.WriteLine();
                        Helper.Delay();
                        Console.WriteLine($"Transaction Complete!");
                        Helper.EnterToContinue();
                        Menu.LoggedInMenu();
                    }
                    else
                    {
                        Console.WriteLine("Transaction Failed, Not Enough Funds");
                        Helper.EnterToContinue();
                        Menu.LoggedInMenu();
                    }
                }
                else if (TransferIndex == 1)
                {
                    List<UserModel> users = PostgresDataAccess.LoadUserModel();
                    int user = Helper.InputIntValidator("Write in the Account Number (ID) you want to transfer to: ");
                    decimal amount = Helper.InputDecimalValidator("Enter amount to transfer: ");
                    int pin = Helper.PinInput("Enter your pin-code to verify: ");
                    int currentUserID = Menu.LoggedInUserID;
                    var currentUser = users.FirstOrDefault(u => u.id == currentUserID);

                    if (pin == currentUser.pin_code)
                    {
                        bool success = PostgresDataAccess.MoneyTransferOther(from_accountId, user, amount);
                        if (success)
                        {
                            Console.WriteLine();
                            Helper.Delay();
                            Console.WriteLine("Transaction complete");
                            Helper.EnterToContinue();
                            Menu.LoggedInMenu();
                        }
                        else
                        {
                            Console.WriteLine("Transaction Failer, Not Enough Funds");
                            Helper.EnterToContinue();
                            Menu.LoggedInMenu();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You did not enter a valid input or incorrect pin");
                        Helper.EnterToContinue();
                        Menu.LoggedInMenu();
                    }
                }
            }
        }

        internal static void ShowBalance()
        {
            AsciiArt.PrintHeader();
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(Menu.LoggedInUserID);
            if (accounts.Count > 0)
            {
                string[] accArray = Helper.GetUserAccountInformation(Menu.LoggedInUserID);
                foreach (string acc in accArray)
                {
                    Console.WriteLine($"\n{acc}");
                }
            }
            else
            {
                Console.WriteLine("No Accounts found!");
            }
            Helper.EnterToContinue();
            Menu.LoggedInMenu();
        }

        internal static void Withdraw()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(Menu.LoggedInUserID);
            string[] accArray = Helper.GetUserAccountInformation(Menu.LoggedInUserID);

            int index = Helper.MenuIndexer(accArray, true);
            if (index == accArray.Length)
            {
                Menu.LoggedInMenu();
            }
            else
            {
                AsciiArt.PrintHeader();
                int accountId = accounts[index].id;
                Console.WriteLine($"You Selected: \n{accArray[index]}\n");
                decimal amount = Helper.InputDecimalValidator("Enter amount to Withdraw: ");
                bool success = PostgresDataAccess.AccountWithdraw(accountId, amount);
                if (success)
                {
                    Console.WriteLine();
                    Helper.Delay();
                    Console.WriteLine($"Withdraw Succesfull!");
                    Helper.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                else
                {
                    Console.WriteLine("Withdraw Failed, Not Enough Moneyz");
                    Helper.EnterToContinue();
                    Menu.LoggedInMenu();
                }
            }
        }
    }
}
