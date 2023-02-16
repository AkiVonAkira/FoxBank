using System.Globalization;
namespace FoxBank;
class Program
{
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        LoadProgram();
    }

    private static void LoadProgram()
    {
        AsciiArt.PrintWelcome();
        Helper.Delay(6000, true);
        Menu.SignInMenu();
    }
}
