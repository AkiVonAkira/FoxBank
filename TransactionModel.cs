namespace FoxBank
{
    internal class TransactionModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int from_account_id { get; set; }
        public int to_account_id { get; set; }
        public DateTime timestamp { get; set; }


    }
}
