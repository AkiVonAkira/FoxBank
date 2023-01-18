namespace FoxBank;
class Program
{
    static void Main(string[] args)
    {
        Menu mainMenu = new Menu(new string[] { "Sign In", "Create New User", "Exit" });
        mainMenu.PrintMenu();

        bool showMenu = true;

        while (showMenu)
        {
            int index = mainMenu.UseMenu();
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
