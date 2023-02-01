using System.Globalization;
namespace FoxBank;
class Program
{
    public int Login { get; set; }
    static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        Menu.SignInMenu();
    }
}
