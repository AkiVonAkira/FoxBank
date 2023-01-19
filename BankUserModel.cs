namespace FoxBank
{
    public class BankUserModel
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pin_code { get; set; }
        public int role_id { get; set; }
        public int branch_id { get; set; }
        public bool is_admin { get; set; }
        public bool is_client { get; set; }
        public List<BankAccountModel> accounts { get; set; }
    }
}
