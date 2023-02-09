namespace FoxBank
{
    internal class AdminMenu
    {
        internal static void LoggedInAdminMenu()
        {
            Menu mainAdminMenu = new Menu(new string[] { "Create User", "View as User", "Open new account", "Sign out" });
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
                    case 1:
                        Menu.LoggedInMenu();
                        break;
                    case 2:
                        Menu.OpenAccount();
                        break;
                    default: break;
                }
            }
        }

        internal static void CreateUser()
        {
            AsciiArt.PrintHeader();
            string firstName = Helper.InputStringValidator("What's the users first name? ");
            string lastName = Helper.InputStringValidator("What's the users last name? ");
            string pinCode = Helper.InputStringValidator("What's the users pin code? ");
            string email = Helper.InputStringValidator("What's the users email adress? ");

            // Load role from db, select role names, and turn it into an array.
            List<BankRoleModel> roles = PostgresDataAccess.LoadBankRoleModel();
            string[] roleArray = roles.Select(role => Helper.FirstCharToUpper(role.name)).ToArray();

            int roleIndex = Helper.MenuIndexer(roleArray, false, "Select Which Permission Role the User Should Have: ");
            int roleId = roles[roleIndex].id;

            // Load branch from db, select branch names, and turn it into an array.
            List<BankBranchModel> branches = PostgresDataAccess.LoadBankBranchModel();
            string[] branchArray = branches.Select(branch => Helper.FirstCharToUpper(branch.name)).ToArray();

            int branchIndex = Helper.MenuIndexer(branchArray, false, "Select Which Branch the User is From: ");
            int branchId = branches[branchIndex].id;

            PostgresDataAccess.CreateUserModel(firstName, lastName, pinCode, roleId, branchId, email);
            Helper.Delay();
            AsciiArt.PrintHeader();
            Console.WriteLine($"\n-------------------------------------------------\n\n" +
                $"Created User: {firstName} {lastName} - With {Helper.FirstCharToUpper(roles[roleIndex].name)} Permissions" +
                $" - From: {branches[branchIndex].name}.\n\n" +
                $"-------------------------------------------------\n");
            Helper.EnterToContinue();
            LoggedInAdminMenu();
        }
    }
}
