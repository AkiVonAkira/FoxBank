namespace FoxBank
{
    internal class AdminMenu
    {
        internal static void Menu()
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

        internal static void CreateUser()
        {
            string firstName = Helper.InputStringValidator("What's the users first name? ");
            string lastName = Helper.InputStringValidator("What's the users last name? ");
            string pinCode = Helper.InputStringValidator("What's the users pin code? ");
            string email = Helper.InputStringValidator("What's the users email adress? ");

            // Load role from db, select role names, and turn it into an array.
            List<BankRoleModel> roles = PostgresDataAccess.LoadBankRoleModel();
            string[] roleArray = roles.Select(role => role.name.ToUpper()).ToArray();

            // menu stuff
            int roleIndex = Helper.MenuIndexer(roleArray);
            int roleId = roles[roleIndex].id;

            // Load branch from db, select branch names, and turn it into an array.
            List<BankBranchModel> branches = PostgresDataAccess.LoadBankBranchModel();
            string[] branchArray = branches.Select(branch => branch.name.ToUpper()).ToArray();

            // menu stuff
            int branchIndex = Helper.MenuIndexer(branchArray);
            int branchId = branches[branchIndex].id;

            Console.Write($"\nCreating User");
            Helper.Delay();

            PostgresDataAccess.CreateUserModel(firstName, lastName, pinCode, roleId, branchId, email);
        }
    }
}
