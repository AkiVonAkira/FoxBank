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
                Console.ForegroundColor = i == _selectedIndex ? ConsoleColor.Cyan : ConsoleColor.White;
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
    }
}
