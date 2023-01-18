namespace FoxBank
{
    internal class BankUserModel
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string pincode { get; set; }

        public string full_name
        {
            get
            {
                return $"{first_name} {last_name}";
            }
        }

    }
}
