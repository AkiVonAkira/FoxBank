namespace FoxBank
{
    internal class AccountModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal balance { get; set; }
        public int interest_rate { get; set; }
        public int user_id { get; set; }
        public int currency_id { get; set; }

    }
}
