namespace FoxBank;
class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Menu class with the options "Sign In", "Create New User", "Exit"
        Menu mainMenu = new Menu(new string[] { "Sign In", "Create New User", "Exit" });
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
                    AdminMenu();
                    break;
                case 1:
                    RandomMethod();
                    break;
                case 2:
                    showMenu = false;
                    break;
                default:
                    break;
            }
        }
    }

    private static void AdminMenu()
    {
        // Create an instance of the Menu class with the options "Fetch new Exchange Rates", "Create New User", "Return"
        Menu adminMenu = new Menu(new string[] { "Fetch new Exchange Rates", "Create New User", "Exit" });
        // Print the menu to the console
        adminMenu.PrintMenu();

        // Declare a variable to keep track of whether to show the menu or not
        bool showMenu = true;

        // Loop until the showMenu variable is false
        while (showMenu)
        {
            // Get the selected index from the UseMenu method
            int index = adminMenu.UseMenu();
            // Check the selected index
            switch (index)
            {
                case 0:
                    RandomMethod();
                    break;
                case 1:
                    RandomMethod();
                    break;
                case 2:
                    showMenu = false;
                    break;
                default:
                    break;
            }
        }
    }

    private static void RandomMethod()
    {
        Console.WriteLine("AAAAAAAAAA");
    }
}
