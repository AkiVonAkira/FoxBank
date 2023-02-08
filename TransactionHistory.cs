namespace FoxBank
{
    public class TransactionHistory
    {
        public static void AccountHistory()
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
                int accountId = accounts[index].id;
                List<TransactionModel> transactionHistories = PostgresDataAccess.TransactionHistory(accountId);
                foreach (TransactionModel transactionHistory in transactionHistories)
                {
                    Console.WriteLine($"{transactionHistory.name}, {transactionHistory.amount}");
                }
                //Console.WriteLine($"\nYou selected {myArray[index]}.");

                Helper.EnterToContinue();

                /*if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("You did not enter a valid input");
                    Helper.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                bool success = PostgresDataAccess.AccountWithdraw(accountId, amount);
                if (success)
                {

                    Console.WriteLine("Withdraw successful");
                    Helper.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                else
                {
                    Console.WriteLine("Withdraw Failed, Not Enough Moneyz");
                    Helper.EnterToContinue();
                    Menu.LoggedInMenu();
                }
                */
            }
        }
    }
}
