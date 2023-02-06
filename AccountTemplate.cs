namespace FoxBank
{
    internal class AccountTemplate
    {
        public string name;
        public decimal interest_rate;
    }

    static class Templates
    {
        public static List<AccountTemplate> list = new List<AccountTemplate>
        {
            new AccountTemplate
            {
                name = "Personkonto",
                interest_rate = 0
            },
            new AccountTemplate
            {
                name = "Sparkonto",
                interest_rate = 1
            },
            new AccountTemplate
            {
                name = "Lönekonto",
                interest_rate = 0
            },
            new AccountTemplate
            {
                name = "Pensionkonto",
                interest_rate = 1
            },
            new AccountTemplate
            {
                name = "Förmånskonto",
                interest_rate = 2
            },
            new AccountTemplate
            {
                name = "ISK",
                interest_rate = 0
            },
        };

        public static string[] getNames()
        {
            return list.Select(item => item.name).ToArray();
        }
    }
}
