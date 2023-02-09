using System.Data;

namespace FoxBank
{
    internal class Menu
    {
        internal static int LoggedInUserID { get; set; }

        // Declare an array of strings to hold the menu items
        private readonly string[] _menuItems;
        // Declare a variable to keep track of the selected menu item
        private int _selectedIndex;

        public Menu(string[] items)
        {
            _menuItems = items;
            _selectedIndex = 0;
        }

        // Method to print the menu to the console
        public void PrintMenu()
        {
            // Clear the console before printing the menu
            Console.Clear();
            // Iterate through the menu items array
            for (int i = 0; i < _menuItems.Length; i++)
            {
                // Set the console color to cyan for the selected menu item and white for the rest
                Console.ForegroundColor = i == _selectedIndex ? ConsoleColor.Green : ConsoleColor.White;
                // Print the menu item with a arrow symbol in front of the selected item
                Console.WriteLine(i == _selectedIndex ? $"↪ {_menuItems[i]}" : $"  {_menuItems[i]}  ");
            }
            // Reset the console color to its default value
            Console.ResetColor();
        }

        // Property to get and set the selected index
        public int SelectIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = (value % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
        }

        // Method to handle user input and navigate the menu
        public int UseMenu()
        {
            // Declare a variable to store the user's input
            ConsoleKey userInput;
            do
            {
                // Read the user's input
                userInput = Console.ReadKey(true).Key;
                // Check the user's input
                switch (userInput)
                {
                    // If the user pressed the up arrow
                    case ConsoleKey.UpArrow:
                        // Decrement the selected index
                        _selectedIndex--;
                        // Make sure the selected index is within the range of the menu items
                        _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
                        break;
                    // If the user pressed the down arrow
                    case ConsoleKey.DownArrow:
                        // Increment the selected index
                        _selectedIndex++;
                        // Make sure the selected index is within the range of the menu items
                        _selectedIndex = (_selectedIndex % _menuItems.Length + _menuItems.Length) % _menuItems.Length;
                        break;
                    // If the user pressed the enter or spacebar key
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        // Store the current index in a variable
                        var index = _selectedIndex;
                        // Reprint the menu
                        PrintMenu();
                        // Return the selected index
                        return index;
                }
                // Reprint the menu
                PrintMenu();
            } while (true);
        }

        //Method that prints signin menu
        internal static void SignInMenu()
        {
            // Create an instance of the Menu class with the options "Sign In", "Create New User", "Exit"
            Menu mainMenu = new Menu(new string[] { "Sign In", "Dev Shortcut", "Exit" });
            // Print the menu to the console
            mainMenu.PrintMenu();

            // Declare a variable to keep track of whether to show the menu or not
            bool showMenu = true;

            // Loop until the showMenu variable is false
            while (showMenu)
            {
                // Get the selected index from the UseMenu method
                int index = mainMenu.UseMenu();
                // Check the selected index
                switch (index)
                {
                    case 0:
                        SignIn();
                        break;
                    case 1:
                        //dev shortcut
                        OpenAccount();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    // If the selected index is none of the above
                    default:
                        // Do nothing
                        break;
                }
            }
        }

        //Method that prints menu when loggedin
        internal static void LoggedInMenu()
        {
            // Create an instance of the Menu class with the options "Sign In", "Create New User", "Exit"
            Menu mainMenu = new Menu(new string[] { "Show balance", "Transfer", "Withdraw", "Transaction History", "Open new account", "Sign out" });
            // Print the menu to the console
            mainMenu.PrintMenu();

            // Declare a variable to keep track of whether to show the menu or not
            bool showMenu = true;

            // Loop until the showMenu variable is false
            while (showMenu)
            {
                // Get the selected index from the UseMenu method
                int index = mainMenu.UseMenu();
                // Check the selected index
                switch (index)
                {
                    case 0:
                        ShowBalance();
                        break;
                    case 1:
                        Helper.Transfer();
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        TransactionHistory.AccountHistory();
                        break;
                    case 4:
                        OpenAccount();
                        break;
                    case 5:
                        // Set the showMenu variable to false to exit the loop
                        showMenu = false;
                        SignInMenu();
                        break;
                    // If the selected index is none of the above
                    default:
                        // Do nothing
                        break;
                }
            }
        }

        internal static void OpenAccount()
        {
            string[] accountArray = AccountTemplates.getNames();
            int accountIndex = Helper.MenuIndexer(accountArray, true);
            if (accountIndex == accountArray.Length) { SignInMenu(); }

            string accountName = accountArray[accountIndex];
            decimal accountInterestRate = AccountTemplates.getRates(accountName);

            List<BankCurrencyModel> currencies = PostgresDataAccess.LoadCurrencyModel();
            string[] currencyArray = currencies.Select(currency => currency.name).ToArray();
            int currencyIndex = Helper.MenuIndexer(currencyArray, true);
            if (accountIndex == accountArray.Length) { OpenAccount(); }

            int accountCurrencyId = currencies[currencyIndex].id;

            decimal balance = Helper.InputDecimalValidator("Please enter the amount you would like in your account: ");

            PostgresDataAccess.CreateAccountModel(accountName, balance, accountInterestRate, LoggedInUserID, accountCurrencyId);
            Helper.Delay();
            Console.WriteLine($"{accountName} Opened with {balance.ToString():n} {currencies[currencyIndex].name}");
            Helper.EnterToContinue();
        }

        //Method to check email and pin when signing in
        internal static void SignIn()
        {
            Console.Clear();
            List<UserModel> users = PostgresDataAccess.LoadUserModel();
            Console.WriteLine("Enter email & password");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Pin: ");
            if (!int.TryParse(Console.ReadLine(), out int pin))
            {
                Console.WriteLine("You did not enter a number");
                Helper.EnterToContinue();
                return;
            }

            int counter = 0;
            //Loops every user in UserModel to check for email and pin match.
            foreach (UserModel user in users)
            {
                counter++;
                if (user.bank_email.Equals(email) && user.pin_code == pin)
                {
                    Helper.Delay();
                    AsciiWelcome.PrintWelcome(user.first_name.ToString(), user.last_name.ToString());
                    LoggedInUserID = user.id;
                    Helper.EnterToContinue();
                    List<BankRoleModel> roles = PostgresDataAccess.LoadBankRoleModel();
                    int userRoleId = user.role_id - 1;
                    if (roles[userRoleId].is_client && !roles[userRoleId].is_admin) // id 2
                    {
                        LoggedInMenu();
                    }
                    else if (!roles[userRoleId].is_client && roles[userRoleId].is_admin) // id 1
                    {
                        AdminMenu.LoggedInAdminMenu();
                    }
                    else if (roles[userRoleId].is_client && roles[userRoleId].is_admin) // id 3
                    {
                        LoggedInMenu();
                    }
                    return;
                }
                if (counter >= users.Count)
                {
                    Console.WriteLine("Email or pin-code was not correct.");
                    Helper.EnterToContinue();
                    return;
                }
            }
        }

        internal static void ShowBalance()
        {
            Console.Clear();
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(LoggedInUserID);
            List<BankCurrencyModel> currencies = PostgresDataAccess.LoadCurrencyModel();
            if (accounts.Count > 0)
            {
                foreach (AccountModel account in accounts)
                {
                    //Console.WriteLine($"{account.name}, {account.currency_id}, {currencies.Count}");
                    int accountCurrencyId = account.currency_id;
                    BankCurrencyModel accountCurrency = currencies.FirstOrDefault(c => c.id == accountCurrencyId);
                    if (accountCurrency != null)
                    {
                        Console.WriteLine($"ID: {account.id} Name: {account.name} Balance: {account.balance:n} {accountCurrency.name}");
                    }
                    else
                    {
                        Console.WriteLine("Currency ID not found");
                    }
                }
            }

            else
            {
                Console.WriteLine("No Accounts found!");
            }
            Helper.EnterToContinue();
            LoggedInMenu();
        }

        internal static void Withdraw()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(LoggedInUserID);
            string[] accArray = Helper.GetUserAccountInformation(LoggedInUserID);

            int index = Helper.MenuIndexer(accArray, true);
            if (index == accArray.Length)
            {
                LoggedInMenu();
            }
            else
            {
                Console.Clear();
                int accountId = accounts[index].id;
                Console.WriteLine($"\nYou selected {accArray[index]}.");
                Console.WriteLine("Enter amount to withdraw: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("You did not enter a valid input");
                    Helper.EnterToContinue();
                    LoggedInMenu();
                }
                bool success = PostgresDataAccess.AccountWithdraw(accountId, amount);
                if (success)
                {

                    Console.WriteLine("Withdraw successful");
                    Helper.EnterToContinue();
                    LoggedInMenu();
                }
                else
                {
                    Console.WriteLine("Withdraw Failed, Not Enough Moneyz");
                    Helper.EnterToContinue();
                    LoggedInMenu();
                }
            }
        }
    }
}
