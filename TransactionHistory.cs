using FoxBank.Models;

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
                AsciiArt.PrintHeader();
                var accountId = accounts[index].id;
                List<TransactionModel> transactionHistories = PostgresDataAccess.TransactionHistory(accountId);

                AsciiArt.PrintHeader();
                foreach (TransactionModel transactionHistory in transactionHistories)
                {
                    if (accountId == transactionHistory.from_account_id)
                    {
                        Console.WriteLine($"{transactionHistory.name}  {transactionHistory.GetSignedAmount(transactionHistory.from_account_id)} Skickat till ID: {transactionHistory.to_account_id}");
                    }
                    else
                    {
                        Console.WriteLine($"{transactionHistory.name}  +{transactionHistory.amount} Skickat från ID: {transactionHistory.from_account_id}");
                    }
                }
                Helper.EnterToContinue();

            }
        }



    }
}
