namespace FoxBank
{
    public static class Helper
    {
        public static bool Transaction()
        {
            Console.WriteLine("Tranfer amount:");
            decimal transfer = decimal.Parse(Console.ReadLine());
            return PostgresDataAccess.MoneyTransfer(Menu.LoggedInUserID, 5, 10, transfer);
        }
    }
}
