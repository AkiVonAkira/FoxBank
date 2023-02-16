using FoxBank.Models;
using System.Diagnostics;

namespace FoxBank
{
    public static class Helper
    {
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

        internal static decimal InputDecimalValidator(string prompt)
        {
            decimal userInput = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.Write(prompt);
                validInput = decimal.TryParse(Console.ReadLine().Replace(',', '.'), out userInput);
                if (!validInput)
                {
                    Console.WriteLine("\nInvalid input. Please try again.");
                }
            }
            return userInput;
        }

        internal static int InputIntValidator(string prompt)
        {
            int userInput = 0;
            bool validInput = false;
            while (!validInput)
            {
                Console.Write(prompt);
                validInput = int.TryParse(Console.ReadLine(), out userInput);
                if (!validInput)
                {
                    Console.WriteLine("\nInvalid input. Please try again.");
                }
            }
            return userInput;
        }

        public static int PinInput(string prompt)
        {
            Console.Write(prompt);
            ConsoleKeyInfo key;
            string pin = "";
            int result;

            do
            {
                key = Console.ReadKey(true);

                // If the key is not a backspace or enter, add it to the PIN string if it is numeric
                if (
                    key.Key != ConsoleKey.Backspace &&
                    key.Key != ConsoleKey.Enter &&
                    char.IsDigit(key.KeyChar)
                )
                {
                    pin += key.KeyChar;
                    Console.Write("•");
                }
                // If the key is a backspace, remove the last character from the PIN string
                else if (key.Key == ConsoleKey.Backspace && pin.Length > 0)
                {
                    pin = pin.Substring(0, pin.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            // Check if the input can be parsed to an integer
            if (int.TryParse(pin, out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                return PinInput(prompt);
            }
        }

        internal static string[] GetUserAccountInformation(int userId)
        {
            // Load the accounts for the logged-in user
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(userId);
            // Load the currency information
            List<BankCurrencyModel> currencies = PostgresDataAccess.LoadCurrencyModel();
            // Create an array of strings containing the account name, balance, and currency name
            string[] accArray = accounts.Select(account =>
            {
                // Find the currency associated with the account
                var currency = currencies.FirstOrDefault(c => c.id == account.currency_id);
                // If a currency was found, return the account information with the currency name
                if (currency != null)
                {
                    return account.name + ": " +
                    String.Format("{0:n}", account.balance) + " " + currency.name;
                }
                // If no currency was found, return the account information with a message indicating that the currency was not found
                else
                {
                    return account.name + ": " +
                    String.Format("{0:n}", account.balance) + " (Currency not found)";
                }
            }).ToArray();
            return accArray;
        }

        // This method returns the index of the selected menu item from an array of strings
        internal static int MenuIndexer(string[] array, bool hasBack = false, string headerText = "")
        {
            // If the 'hasBack' flag is set to true, add a "Go Back" option to the end of the menu
            if (hasBack)
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = "Go Back";
            }
            // Create a new instance of the 'Menu' class with the array of menu items
            Menu menu = new Menu(array);
            // Print the menu
            if (!string.IsNullOrEmpty(headerText))
            {
                menu.PrintMenu(headerText);
            }
            else
            {
                menu.PrintMenu();
            }
            // Get the selected index using the 'UseMenu' method
            int index = menu.UseMenu();
            // Return the selected index
            return index;
        }

        internal static void Delay(int waitTime = 2625, bool canSkip = false)
        {
            int maxDots = 15;
            int dotDelay = (waitTime / maxDots) - 1;
            int elapsedTime = 0;
            int dotsPrinted = 0;

            Stopwatch stopwatch = Stopwatch.StartNew();

            while (elapsedTime < waitTime)
            {
                if (canSkip)
                {
                    // If a key has been pressed, break out of the loop
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true); // Read the key and discard it
                        break;
                    }
                }
                Console.Write(".");
                dotsPrinted++;
                elapsedTime = (int)stopwatch.ElapsedMilliseconds;

                if (elapsedTime >= waitTime || dotsPrinted >= maxDots)
                {
                    break;
                }

                Thread.Sleep(dotDelay);
            }

            Console.Write("\u2713\n\n");
            Thread.Sleep(600);
        }

        internal static void EnterToContinue()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n↪ Press Enter to Continue...");
            Console.ResetColor();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
        }

        internal static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
