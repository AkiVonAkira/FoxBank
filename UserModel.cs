namespace FoxBank
{
    internal class UserModel
    {
        public int id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? bank_email { get; set; }
        public int pin_code { get; set; }
        public int role_id { get; set; }
        public int branch_id { get; set; }

    }
}
