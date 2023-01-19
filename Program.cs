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
                    Login();
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

    private static void Login()
    {
        List<BankUserModel> users = PostgresDataAccess.LoadBankUsers();
        Console.WriteLine($"users length: {users.Count}");
        foreach (BankUserModel user in users)
        {
            Console.WriteLine($"Hello {user.first_name} your pincode is {user.pin_code}");
        }
        while (true)
        {
            Console.Write("Please enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter your pin code: ");
            string pinCode = Console.ReadLine();

            List<BankUserModel> checkedUsers = PostgresDataAccess.CheckLogin(firstName, pinCode);
            if (checkedUsers.Count < 1)
            {
                Console.WriteLine("Login failed.");
            }
            foreach (BankUserModel user in checkedUsers)
            {
                user.accounts = PostgresDataAccess.GetUserAccounts(user.id);
                Console.WriteLine($"Logged in as {user.first_name} your pincode is {user.pin_code} and the ID is {user.id}");
                Console.WriteLine($"role_id: {user.role_id}  branch_id: {user.branch_id}");
                Console.WriteLine($"is_admin: {user.is_admin}  is_clien: {user.is_client}");
                Console.WriteLine($"User account list length: {user.accounts.Count}");
                if (user.accounts.Count > 0)
                {
                    foreach (BankAccountModel account in user.accounts)
                    {
                        Console.WriteLine($"ID: {account.id}  Account name: {account.name}  Balance: {account.balance}");
                        Console.WriteLine($"Currency: {account.currency_name}  Exchange rate; {account.currency_exchange_rate}");
                    }
                }
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
