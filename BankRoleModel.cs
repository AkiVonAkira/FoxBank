namespace FoxBank
{
    internal class BankRoleModel
    {
        public int id { get; set; }
        public string? name { get; set; }
        public bool is_admin { get; set; }
        public bool is_client { get; set; }
    }
}
