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
            // Print a message to prompt the user to select an option
            Console.WriteLine("Please select an option:");
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
                    // If the selected index is 0 (Sign In)
                    case 0:
                        // Call the RandomMethod() method
                        Menu.SignIn();
                        break;
                    case 1:
                        CreateUser();
                        break;
                    // If the selected index is 2 (Exit)
                    case 2:
                        // Set the showMenu variable to false to exit the loop
                        Environment.Exit(0);
                        break;
                    // If the selected index is none of the above
                    default:
                        // Do nothing
                        break;
                }
            }
        }

        private static void AdminMenu()
        {
            Menu mainAdminMenu = new Menu(new string[] { "Create User", "View User", "Create new account", "Sign out" });
            mainAdminMenu.PrintMenu();

            bool showAdminMenu = true;

            while (showAdminMenu)
            {
                int index = mainAdminMenu.UseMenu();

                switch (index)
                {
                    case 0:
                        CreateUser();
                        break;
                    default: break;
                }
            }
        }

        private static void CreateUser()
        {
            string firstName = InputStringValidator("What's the users first name? ");
            string lastName = InputStringValidator("What's the users last name? ");
            string pinCode = InputStringValidator("What's the users pin code? ");
            string email = InputStringValidator("What's the users email adress? ");

            // Load role from db, select role names, and turn it into an array.
            List<BankRoleModel> roles = PostgresDataAccess.LoadBankRoleModel();
            string[] roleArray = roles.Select(role => role.name).ToArray();

            // menu stuff
            Menu roleMenu = new Menu(roleArray);
            roleMenu.PrintMenu();
            int roleIndex = roleMenu.UseMenu();
            int roleId = roles[roleIndex].id;

            Console.ReadLine();

            // Load branch from db, select branch names, and turn it into an array.
            List<BankBranchModel> branches = PostgresDataAccess.LoadBankBranchModel();
            string[] branchArray = branches.Select(branch => branch.name).ToArray();

            // menu stuff
            Menu branchMenu = new Menu(branchArray);
            branchMenu.PrintMenu();
            int branchIndex = branchMenu.UseMenu();
            int branchId = branches[branchIndex].id;

            PostgresDataAccess.CreateUserModel(firstName, lastName, pinCode, roleId, branchId, email);
        }

        //Method that prints menu when loggedin
        internal static void LoggedInMenu()
        {
            // Create an instance of the Menu class with the options "Sign In", "Create New User", "Exit"
            Menu mainMenu = new Menu(new string[] { "Show balance", "Transfer", "Withdraw", "Create new account", "Sign out" });
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
                    // If the selected index is 0 (Sign In)
                    case 0:
                        //ShowBalance();
                        ShowBalance();
                        break;
                    // If the selected index is 1 (Create New User)
                    case 1:

                        Helper.WhichAccount();
                        // Call the RandomMethod() method
                        //Transfer();
                        break;
                    // If the selected index is 2 (Exit)
                    case 2:
                        // Call the RandomMethod() method
                        Withdraw();
                        break;
                    case 3:
                        // Call the RandomMethod() method
                        //CreateNewAccount();
                        break;
                    case 4:
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
                EnterToContinue();
                return;
            }

            int counter = 0;
            //Loops every user in UserModel to check for email and pin match.
            foreach (UserModel user in users)
            {
                counter++;
                if (user.bank_email.Equals(email) && user.pin_code == pin)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome to FOX BANK {user.first_name} {user.last_name}");
                    LoggedInUserID = user.id;
                    EnterToContinue();
                    if (user.role_id != 1) { LoggedInMenu(); } else { AdminMenu(); }
                    return;
                }
                if (counter >= users.Count)
                {
                    Console.WriteLine("Email or pin-code was not correct.");
                    EnterToContinue();
                    return;
                }
            }
        }

        internal static void ShowBalance()
        {
            Console.Clear();
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(LoggedInUserID);
            if (accounts.Count > 0)
            {
                foreach (AccountModel account in accounts)
                {
                    Console.WriteLine($"ID: {account.id} Name: {account.name} Balance: {account.balance}");
                }
            }
            else
            {
                Console.WriteLine("No Accounts found!");
            }
            EnterToContinue();
            LoggedInMenu();
        }

        internal static void Withdraw()
        {
            List<AccountModel> accounts = PostgresDataAccess.LoadUserAccount(LoggedInUserID);

            string[] myArray = accounts.Select(account => account.name).ToArray();
            Array.Resize(ref myArray, myArray.Length + 1);
            myArray[myArray.Length - 1] = "Back";

            Menu balanceMenu = new Menu(myArray);
            balanceMenu.PrintMenu();
            int index = balanceMenu.UseMenu();

            if (index + 1 == myArray.Length)
            {
                LoggedInMenu();
            }
            else
            {
                Console.Clear();
                int accountId = accounts[index].id;
                Console.WriteLine($"\nYou selected {myArray[index]}.");
                Console.WriteLine("Enter amount to withdraw: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                {
                    Console.WriteLine("You did not enter a valid input");
                    EnterToContinue();
                    LoggedInMenu();
                }
                bool success = PostgresDataAccess.AccountWithdraw(accountId, amount);
                if (success)
                {

                    Console.WriteLine("Withdraw successful");                
                    EnterToContinue();
                    LoggedInMenu();

                }
                else
                {
                    Console.WriteLine("Withdraw Failed, Not Enough Moneyz");
                    EnterToContinue();
                    LoggedInMenu();
                }
            }
        }

        internal static void EnterToContinue()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }

        private static string InputStringValidator(string prompt)
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
    }
}
