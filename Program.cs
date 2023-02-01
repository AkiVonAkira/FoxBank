using System.Globalization;
namespace FoxBank;
class Program
{
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Menu.SignInMenu();
    }
}
