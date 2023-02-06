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
