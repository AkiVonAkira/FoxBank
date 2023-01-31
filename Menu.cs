namespace FoxBank
{
    internal class Menu
    {
        // Declare an array of strings to hold the menu items
        private readonly string[] _menuItems;
        // Declare a variable to keep track of the selected menu item
        private int _selectedIndex;

        // Constructor that takes an array of strings as a parameter, 
        // initializes the _menuItems array with the passed in array
        // and sets the initial selected index to 0
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
            Menu mainMenu = new Menu(new string[] { "Sign In", "Exit" });
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
                    // If the selected index is 2 (Exit)
                    case 1:
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
                        // Call the RandomMethod() method
                        //ShowBalance();
                        break;
                    // If the selected index is 1 (Create New User)
                    case 1:
                        // Call the RandomMethod() method
                        //Transfer();
                        break;
                    // If the selected index is 2 (Exit)
                    case 2:
                        // Call the RandomMethod() method
                        //Withdraw();
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
            List<UserModel> users = PostgresDataAccess.LoadUserModel();
            Console.WriteLine("Enter email & password");
            Console.WriteLine("Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Pin: ");
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
                    Console.WriteLine($"Welcome to FOX BANK {user.first_name} {user.last_name}");
                    EnterToContinue();
                    //Create if-statement to determine client or admin menu.
                    //if client
                    LoggedInMenu();
                    //if admin
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

        internal static void EnterToContinue()
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
        }
    }

}
